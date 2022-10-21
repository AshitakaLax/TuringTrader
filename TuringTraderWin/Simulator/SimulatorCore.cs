using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.Algorithm;
using TuringTraderWin.Calendar;
using TuringTraderWin.DataSource;
using TuringTraderWin.DataStructures;
using TuringTraderWin.Extensions;
using TuringTraderWin.Instruments;
using TuringTraderWin.Optimizer;
using TuringTraderWin.Orders;

namespace TuringTraderWin.Simulator
{
  public class SimulatorCore : ISimulatorCore
  {
    /// <summary>
    /// The Logger.
    /// </summary>
    private readonly ILogger Logger;

    /// <summary>
    /// The Data Source Manager.
    /// </summary>
    private readonly IDataSourceManager DataSourceManager;

    /// <summary>
    /// The Instrument Manager.
    /// </summary>
    private readonly IInstrumentManager InstrumentManager;

    /// <summary>
    /// The OptimizerManager.
    /// </summary>
    private readonly IOptimizerManager OptimizerManager;
   
    /// <summary>
    /// The Transaction History.
    /// </summary>
    private readonly ITransactionHistory TransactionHistory;

    public SimulatorCore(ILogger<SimulatorCore> logger, IDataSourceManager dataSourceManager, IInstrumentManager instrumentManager, IOptimizerManager optimizerManager, ITransactionHistory transactionHistory)
    {
      Logger = logger;
      DataSourceManager = dataSourceManager;
      InstrumentManager = instrumentManager;
      OptimizerManager = optimizerManager;
      TransactionHistory = transactionHistory;

      //TODO: update this to be configurable smoothly.
      SimulatorPortfolioInfo = new SimulatorPortfolioInfo();
    }

    /// <inheritdoc/>
    public TimeSeries<double> NetAssetValue { get; set; }

    /// <inheritdoc/>
    public ISimulatorPortfolioInfo SimulatorPortfolioInfo { get; set; }

    /// <inheritdoc/>
    public string Name { get; set; }

    /// <inheritdoc/>
    public DateTime StartTime { get; set; }

    /// <inheritdoc/>
    public DateTime? WarmupStartTime { get; set; }

    /// <inheritdoc/>
    public DateTime EndTime { get; set; }

    /// <inheritdoc/>
    public int TradingDays { get; set; }

    /// <inheritdoc/>
    public TimeSeries<DateTime> SimTimes { get; set; } = new TimeSeries<DateTime>();

    /// <inheritdoc/>
    public bool IsLastBar { get; set; }

    /// <inheritdoc/>
    public List<Order> PendingOrders { get; set; } = new List<Order>();

    /// <inheritdoc/>
    public IAlgorithm Algorithm { get; set; }

    /// <inheritdoc/>
    public IEnumerable<AlgorithmParameter> AlgorithmParameters { get; set; }

    /// <inheritdoc/>
    public long CurrentTradingBar { get; set; }

    /// <inheritdoc/>
    public DateTime CalcNextSimTime(DateTime timestamp)
    {
      return HolidayCalendar.NextLiveSimTime(timestamp);
    }

    /// <inheritdoc/>
    public virtual void Deposit(double amount)
    {
      if (amount < 0.0)
        throw new Exception("SimulatorCore: Deposit w/ negative amount");

      if (amount > 0.0)
      {
        Order order = new Order()
        {
          Instrument = null,
          Quantity = -1,
          Type = OrderType.cash,
          Price = amount,
        };
        SimulatorPortfolioInfo.CashContributions += amount;
        QueueOrder(order);
      }
    }

    /// <inheritdoc/>
    public virtual double FillModel(Order orderTicket, Bar barOfExecution, double theoreticalPrice)
    {
      return theoreticalPrice;
    }

    public string GenerateSimulatorReport()
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"SIMULATION: {this.Name}");
      sb.AppendLine("Simulation with Instruments:");
      sb.AppendLine(String.Join(',', InstrumentManager.Positions.Keys.Select(instrument => instrument.Ticker)));
      sb.AppendLine($"Total Contributions:{SimulatorPortfolioInfo.CashContributions.PrintCash()}");
      sb.AppendLine($"Current Value:{SimulatorPortfolioInfo.Cash.PrintCash()}");
      return sb.ToString();
    }


    /// <inheritdoc/>
    public virtual double GetNetAssetValue()
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public virtual void InitializeSimTimes()
    {
      CurrentTradingBar = 0;
      // This is equivalent to the SimTimes Getter.
      if (WarmupStartTime == null || WarmupStartTime > StartTime)
      {
        WarmupStartTime = StartTime;
      }

      IDataSource dataSource = DataSourceManager.GetDataSource();
      if(dataSource == null)
      {
        //TODO create custom exception that has references/strings for the name of the algorithm.
        throw new InvalidOperationException("A Data Source is required");
      }

      //IEnumerable<Bar> data = dataSource.LoadData(StartTime, EndTime);
      TradingDays = 0;
      SimulatorPortfolioInfo.Cash = 0.0;
      NetAssetValue = new TimeSeries<double>(-1)
      {
        Value = SimulatorPortfolioInfo.Cash
      };
      SimulatorPortfolioInfo.NetAssetValueHighestHigh = 0.0;
      SimulatorPortfolioInfo.NetAssetValueMaxDrawdown = 1e-10;

      // Clear all previous simulations. I don't think this is necessary in this case, since 
      // every algorithm will be a new simulation, and we can cache the results to generate
      // the reports everytime without running the simulation.

    }


    /// <inheritdoc/>
    public bool IsValidBar(Bar bar)
    {
      if (bar.HasBidAsk)
      {
        if (bar.BidVolume <= 0
                || bar.AskVolume <= 0
                || bar.Bid < 0.2 * bar.Ask)
          return false;
      }

      return true;
    }

    /// <inheritdoc/>
    public bool IsValidSimTime(DateTime timestamp)
    {
      if (timestamp.DayOfWeek >= DayOfWeek.Monday
      && timestamp.DayOfWeek <= DayOfWeek.Friday)
      {
        if (timestamp.TimeOfDay.TotalHours >= 9.5
        && timestamp.TimeOfDay.TotalHours <= 16.0)
          return true;
      }

      return false;
    }

    /// <inheritdoc/>
    public void QueueOrder(Order order)
    {
      order.QueueTime = SimTimes.BarsAvailable > 0 ? SimTimes[0] : default;
      PendingOrders.Add(order);
    }

    /// <inheritdoc/>
    public void RunSimulator(CancellationToken cancellationToken)
    {
      // iterate through the time sim's like the other approach does.
      long numberOfSteps = DataSourceManager.GetNumberOfRunSteps();
      DateTime requestedDay = StartTime;
      for (CurrentTradingBar = 0; CurrentTradingBar < numberOfSteps; CurrentTradingBar++)
      {
        //IEnumerable<Bar> singleDayBars = DataSourceManager.GetDataSource().GetFollowingBars(requestedDay);
        
        //ConcurrentDictionary<string, Bar> singlePeriodData = new ConcurrentDictionary<string, Bar>();
        //foreach(Bar bar in singleDayBars)
        //{
        //  singlePeriodData[bar.Symbol] = bar;
        //}

        //if(!singleDayBars.Any())
        //{
        //  //TODO Enhancement to update by bar frequency.e
        //  requestedDay = requestedDay.AddDays(1.0);
        //  continue;
        //}

        //// update by one day to get the next bar.
        //requestedDay = singleDayBars.First().Time.AddDays(1.0);

        // There are 2 different ideal approachs for handling all potential inputs
        // The ideal would be to utilize an Index of the point where we are in the data structure, while Not accessing the data prior to it.
        // This approach will provide a very performant approach once all of the data is loaded into the dataManager.

        Algorithm.HandleBarIncrement(DataSourceManager.Data, (int)CurrentTradingBar, this, InstrumentManager);
        //Algorithm.HandleBarIncrement(singlePeriodData, this, InstrumentManager);

        if (cancellationToken.IsCancellationRequested)
        {
          Logger.LogWarning($"Algorithm({Algorithm.Info.Name}) has been Cancelled!");
          return;
        }

        // handle orders
        ExecuteOrders();
        PendingOrders.Clear();
      }
      //close out all positions with the closing value of the last bar.
      CloseAllPositons();
    }

    public void Withdraw(double amount)
    {
      if (amount < 0.0)
        throw new Exception("SimulatorCore: Withdraw w/ negative amount");

      if (amount > 0.0)
      {
        Order order = new Order()
        {
          Instrument = null,
          Quantity = 1,
          Type = OrderType.cash,
          Price = amount,
        };

        QueueOrder(order);
      }
    }

    /// <summary>
    /// Closes all of the positions and updates the portfolio.
    /// </summary>
    private void CloseAllPositons()
    {
      foreach(IInstrument instrument in DataSourceManager.Data.Keys)
      {
        // Check whether there are any positions with the specific instrument.
        if(!InstrumentManager.Positions.ContainsKey(instrument))
        {
          continue;
        }

        Bar lastBar = DataSourceManager.Data[instrument].Last();
        Transaction transaction = new Transaction()
        {
          Symbol = instrument.Ticker,
          Type = TransactionType.Equity,
          Order = new Order()
          {
            BarOfExecution = lastBar,
            Instrument = instrument,
            IsBuy = false,
            Price = lastBar.Close,
            Quantity = InstrumentManager.Positions[instrument],
            QueueTime = lastBar.Time,
            Type = OrderType.endOfSimFakeClose
          },
          BarOfExecution = lastBar,
          FillPrice = lastBar.Close,
          Commission = 0.0
        };

        SimulatorPortfolioInfo.Cash = SimulatorPortfolioInfo.Cash + (transaction.FillPrice * transaction.Order.Quantity);

        TransactionHistory.AddTransaction(transaction);
      }
    }

    private void ExecuteOrders()
    {
      foreach (Order ticket in PendingOrders)
      {
        if (ticket.Type == OrderType.cash)
        {
          // to make things similar to stocks, a positive quantity
          // results in a debit, a negative quantity in a credit
          SimulatorPortfolioInfo.Cash -= ticket.Quantity * ticket.Price;
          

          Transaction cashTransaction = new Transaction()
          {
            Symbol = "N/A",
            Type = TransactionType.Cash,
            Order = ticket,
            FillPrice = ticket.Price,
            Commission = 0.0
          };
          TransactionHistory.AddTransaction(cashTransaction);
          //LogEntry l = new LogEntry()
          //{
          //  Symbol = "N/A",
          //  InstrumentType = LogEntryInstrument.Cash,
          //  OrderTicket = ticket,
          //  BarOfExecution = Instruments
          //        .Where(i => i.Time[0] == SimTime[0])
          //        .First()[0],
          //  FillPrice = ticket.Price,
          //  Commission = 0.0,
          //};
          //Log.Add(l);

          continue;//move on to the next order.
        }

        // no trades during warmup phase
        //if (SimTimes[0] < StartTime)
        //  return;

        // conditional orders: cancel, if condition not met
        if (ticket.Condition != null
        && !ticket.Condition(ticket.Instrument))
          return;
        
        // if it is a sell, and we have no positions go to the next order
        if(!ticket.IsBuy && !InstrumentManager.Positions.Any())
        {
          continue;
        }

        IInstrument instrument = ticket.Instrument;
        Bar execBar = null;
        DateTime execTime = default;
        double price = 0.00;
        switch (ticket.Type)
        {
          //----- user transactions
          case OrderType.closeThisBar:
            execBar = ticket.BarOfExecution;
            execTime = SimTimes[1];
            price = execBar.HasBidAsk
                ? (ticket.Quantity > 0 ? execBar.Ask : execBar.Bid)
                : execBar.Close;
            break;

          case OrderType.openNextBar:
            if(CurrentTradingBar+1 >= DataSourceManager.GetNumberOfRunSteps())
            {
              continue;// can't make the next trade since we don't know what the next bar is. TODO: enhancement to add new trailing data.
            }

            // Use the next bar to execute the trade.
            Bar nextBar = DataSourceManager.Data[ticket.Instrument][(int)CurrentTradingBar + 1];
            if (ticket.Quantity == 0 && ticket.IsBuy)
            {
              // get the most shares we can afford.
              ticket.Quantity = (int)(SimulatorPortfolioInfo.Cash / nextBar.Open);
            }
            else if(ticket.Quantity == 0)
            {
              // sell out all of the positon.
              ticket.Quantity = (int)InstrumentManager.Positions[ticket.Instrument];
            }

            execBar = ticket.BarOfExecution;
            execTime = nextBar.Time;
            price = nextBar.Open;
            //price = execBar.HasBidAsk
            //    ? (ticket.Quantity > 0 ? execBar.Ask : execBar.Bid)
            //    : execBar.Open;
            break;

          case OrderType.stopNextBar:
            execBar = ticket.BarOfExecution;
            execTime = SimTimes[0];
            if (ticket.Quantity > 0)
            {
              if (ticket.Price > execBar.High)
                return;

              price = Math.Max(ticket.Price, execBar.Open);
            }
            else
            {
              if (ticket.Price < execBar.Low)
                return;

              price = Math.Min(ticket.Price, execBar.Open);
            }
            break;

          case OrderType.limitNextBar:
            execBar = ticket.BarOfExecution;
            execTime = SimTimes[0];
            if (ticket.Quantity > 0)
            {
              if (ticket.Price < execBar.Low)
                return;

              price = Math.Min(ticket.Price, execBar.Open);
            }
            else
            {
              if (ticket.Price > execBar.High)
                return;

              price = Math.Max(ticket.Price, execBar.Open);
            }
            break;

          //----- simulator-internal transactions

          case OrderType.instrumentDelisted:
          case OrderType.endOfSimFakeClose:
            execBar = ticket.BarOfExecution;
            execTime = SimTimes[0];
            // Original price = execBar.HasBidAsk ? (instrument.Position > 0 ? execBar.Bid : execBar.Ask) : execBar.Close;
            price = execBar.HasBidAsk? (false ? execBar.Bid : execBar.Ask) : execBar.Close;
            break;

          case OrderType.optionExpiryClose:
            // execBar = instrument[0]; // option bar
            execBar = ticket.BarOfExecution; // original _instruments[instrument.OptionUnderlying][0]; // underlying bar
            execTime = SimTimes[1];
            price = ticket.Price;
            break;

          default:
            throw new Exception("SimulatorCore.ExecOrder: unknown order type");
        }


        // run fill model. default fill is theoretical price
        var fillPrice = price;
        //var fillPrice = ticket.Type == OrderType.cash
        //    || ticket.Type == OrderType.optionExpiryClose
        //        ? price
        //        : FillModel(ticket, execBar, price);

        // adjust position, unless its the end-of-sim order
        // this is to ensure that the Positions collection can
        // be queried after the simulation finished
        // LB: this doesn't seem very clean, but I'm sure I'll run into this issue later.
        //if (ticket.Type != OrderType.endOfSimFakeClose)
        //{
        //  if (!Positions.ContainsKey(instrument))
        //    Positions[instrument] = 0;
        //  Positions[instrument] += ticket.Quantity;
        //  if (Positions[instrument] == 0)
        //    Positions.Remove(instrument);
        //}

        // determine # of shares
        int numberOfShares = instrument.IsOption
            ? 100 * ticket.Quantity
            : ticket.Quantity;

        // determine commission (no commission on expiry, delisting, end-of-sim)
        double commission = ticket.Type != OrderType.optionExpiryClose
                        && ticket.Type != OrderType.instrumentDelisted
                        && ticket.Type != OrderType.endOfSimFakeClose
            ? Math.Abs(numberOfShares * SimulatorPortfolioInfo.CommissionPerShare)
            : 0.00;


        // Nothing to buy
        if (numberOfShares == 0)
        {
          continue;
        }

        // pay for it, unless it's the end-of-sim order
        // same reasoning as for adjustment of position applies
        //if (ticket.Type != OrderType.endOfSimFakeClose)
        //{
        //  SimulatorPortfolioInfo.Cash = SimulatorPortfolioInfo.Cash
        //      - numberOfShares * fillPrice
        //      - commission;
        //}

        // add log entry
        Transaction transaction = new Transaction()
        {
          Symbol = ticket.Instrument.Ticker,
          Type = TransactionType.Equity,
          //Type = ticket.Instrument.IsOption
          //      ? (ticket.Instrument.IsOptionPut ? TransactionType.OptionPut : TransactionType.OptionCall)
          //      : TransactionType.Equity,
          Order = ticket,
          BarOfExecution = execBar,
          FillPrice = fillPrice,
          Commission = commission
        };

        if(ticket.IsBuy)
        {
          // get existing shares if there are any
          if(InstrumentManager.Positions.ContainsKey(ticket.Instrument))
          {
            InstrumentManager.Positions[ticket.Instrument] += numberOfShares;
          }
          else
          {
            InstrumentManager.Positions[ticket.Instrument] = numberOfShares;
          }
          SimulatorPortfolioInfo.Cash = SimulatorPortfolioInfo.Cash
              - numberOfShares * fillPrice
              - commission;
        }
        else
        {
          InstrumentManager.Positions[ticket.Instrument] = InstrumentManager.Positions[ticket.Instrument] - numberOfShares;
          SimulatorPortfolioInfo.Cash = SimulatorPortfolioInfo.Cash
              + numberOfShares * fillPrice
              - commission;
        }
        TransactionHistory.AddTransaction(transaction);
      }
    }
  }
}
