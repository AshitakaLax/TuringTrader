using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.Algorithm;
using TuringTraderWin.DataSource;
using TuringTraderWin.DataStructures;
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

    private readonly IDataSourceManager DataSourceManager;

    private readonly IInstrumentManager InstrumentManager;

    private readonly IOptimizerManager OptimizerManager;
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

    public TimeSeries<double> NetAssetValue { get; set; }
    public ISimulatorPortfolioInfo SimulatorPortfolioInfo { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? WarmupStartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int TradingDays { get; set; }
    public TimeSeries<DateTime> SimTimes { get; set; }
    public bool IsLastBar { get; set; }
    public List<IOrder> PendingOrders { get; set; } = new List<IOrder>();
    public IAlgorithm Algorithm { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IEnumerable<AlgorithmParameter> AlgorithmParameters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public DateTime CalcNextSimTime(DateTime timestamp)
    {
      throw new NotImplementedException();
    }

    public void Deposit(double amount)
    {
      throw new NotImplementedException();
    }

    public double FillModel(Order orderTicket, Bar barOfExecution, double theoreticalPrice)
    {
      throw new NotImplementedException();
    }

    public double GetNetAssetValue()
    {
      throw new NotImplementedException();
    }

    public void InitializeSimTimes()
    {
      // This is equivalent to the SimTimes Getter.
      if(WarmupStartTime == null || WarmupStartTime > StartTime)
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

    public bool IsValidBar(Bar bar)
    {
      throw new NotImplementedException();
    }

    public bool IsValidSimTime(DateTime timestamp)
    {
      throw new NotImplementedException();
    }

    public void QueueOrder(IOrder order)
    {
      order.QueueTime = SimTimes.BarsAvailable > 0 ? SimTimes[0] : default;
      PendingOrders.Add(order);
    }

    public void RunSimulator(CancellationToken cancellationToken)
    {
      // iterate through the time sim's like the other approach does.

      //iterate through the Data provided by the Data Source.
      IEnumerable<Bar> data = DataSourceManager.GetDataSource().LoadData(StartTime, EndTime);

      foreach (Bar bar in data)
      {
        Algorithm.HandleBarIncrement(bar, this);
        if (cancellationToken.IsCancellationRequested)
        {
          Logger.LogWarning($"Algorithm({Algorithm.Info.Name}) has been Cancelled!");
          return;
        }

        // handle orders
        ExecuteOrders();
      }
    }

    public void Withdraw(double amount)
    {
      throw new NotImplementedException();
    }

    private void ExecuteOrders()
    {
      foreach (IOrder ticket in PendingOrders)
      {
        if (ticket.Type == OrderType.cash)
        {
          // to make things similar to stocks, a positive quantity
          // results in a debit, a negative quantity in a credit
          SimulatorPortfolioInfo.Cash -= ticket.Quantity * ticket.Price;

          Transaction transaction = new Transaction()
          {
            Symbol = "N/A",
            Type = TransactionType.Cash,
            Order = ticket,
            FillPrice = ticket.Price,
            Commission = 0.0
          };
          TransactionHistory.AddTransaction(transaction);
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

          return;
        }

        // no trades during warmup phase
        if (SimTimes[0] < StartTime)
          return;

        // conditional orders: cancel, if condition not met
        if (ticket.Condition != null
        && !ticket.Condition(ticket.Instrument))
          return;

        IInstrument instrument = ticket.Instrument;
        Bar execBar = null;
        DateTime execTime = default;
        double price = 0.00;
        switch (ticket.Type)
        {
          //----- user transactions
          case OrderType.closeThisBar:
            execBar = instrument[1];
            execTime = SimTimes[1];
            price = execBar.HasBidAsk
                ? (ticket.Quantity > 0 ? execBar.Ask : execBar.Bid)
                : execBar.Close;
            break;

          case OrderType.openNextBar:
            execBar = instrument[0];
            execTime = SimTimes[0];
            price = execBar.HasBidAsk
                ? (ticket.Quantity > 0 ? execBar.Ask : execBar.Bid)
                : execBar.Open;
            break;

          case OrderType.stopNextBar:
            execBar = instrument[0];
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
            execBar = instrument[0];
            execTime = SimTime[0];
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
            execBar = instrument[0];
            execTime = SimTime[0];
            price = execBar.HasBidAsk
                ? (instrument.Position > 0 ? execBar.Bid : execBar.Ask)
                : execBar.Close;
            break;

          case OrderType.optionExpiryClose:
            // execBar = instrument[0]; // option bar
            execBar = _instruments[instrument.OptionUnderlying][0]; // underlying bar
            execTime = SimTime[1];
            price = ticket.Price;
            break;

          default:
            throw new Exception("SimulatorCore.ExecOrder: unknown order type");
        }


        // run fill model. default fill is theoretical price
        var fillPrice = ticket.Type == OrderType.cash
            || ticket.Type == OrderType.optionExpiryClose
                ? price
                : FillModel(ticket, execBar, price);

        // adjust position, unless its the end-of-sim order
        // this is to ensure that the Positions collection can
        // be queried after the simulation finished
        if (ticket.Type != OrderType.endOfSimFakeClose)
        {
          if (!Positions.ContainsKey(instrument))
            Positions[instrument] = 0;
          Positions[instrument] += ticket.Quantity;
          if (Positions[instrument] == 0)
            Positions.Remove(instrument);
        }

        // determine # of shares
        int numberOfShares = instrument.IsOption
            ? 100 * ticket.Quantity
            : ticket.Quantity;

        // determine commission (no commission on expiry, delisting, end-of-sim)
        double commission = ticket.Type != OrderType.optionExpiryClose
                        && ticket.Type != OrderType.instrumentDelisted
                        && ticket.Type != OrderType.endOfSimFakeClose
            ? Math.Abs(numberOfShares * CommissionPerShare)
            : 0.00;

        // pay for it, unless it's the end-of-sim order
        // same reasoning as for adjustment of position applies
        if (ticket.Type != OrderType.endOfSimFakeClose)
        {
          Cash = Cash
              - numberOfShares * fillPrice
              - commission;
        }

        // add log entry
        LogEntry log = new LogEntry()
        {
          Symbol = ticket.Instrument.Symbol,
          InstrumentType = ticket.Instrument.IsOption
                ? (ticket.Instrument.OptionIsPut ? LogEntryInstrument.OptionPut : LogEntryInstrument.OptionCall)
                : LogEntryInstrument.Equity,
          OrderTicket = ticket,
          BarOfExecution = execBar,
          FillPrice = fillPrice,
          Commission = commission,
        };
        // do not remove instrument here, is required for MFE/ MAE analysis
        //ticket.Instrument = null; // the instrument holds the data source... which consumes lots of memory
        Log.Add(log);
      }
    }
  }
}
