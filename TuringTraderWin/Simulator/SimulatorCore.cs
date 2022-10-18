﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.Algorithm;
using TuringTraderWin.Calendar;
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
    public List<IOrder> PendingOrders { get; set; } = new List<IOrder>();

    /// <inheritdoc/>
    public IAlgorithm Algorithm { get; set; }

    /// <inheritdoc/>
    public IEnumerable<AlgorithmParameter> AlgorithmParameters { get; set; }

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

        QueueOrder(order);
      }
    }

    /// <inheritdoc/>
    public virtual double FillModel(IOrder orderTicket, Bar barOfExecution, double theoreticalPrice)
    {
      return theoreticalPrice;
    }


    /// <inheritdoc/>
    public virtual double GetNetAssetValue()
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public virtual void InitializeSimTimes()
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
    public void QueueOrder(IOrder order)
    {
      order.QueueTime = SimTimes.BarsAvailable > 0 ? SimTimes[0] : default;
      PendingOrders.Add(order);
    }

    /// <inheritdoc/>
    public void RunSimulator(CancellationToken cancellationToken)
    {
      // iterate through the time sim's like the other approach does.

      long numberOfSteps = DataSourceManager.GetDataSource().GetNumberOfTimeSteps();
      DateTime requestedDay = StartTime;
      for (int i = 0; i < numberOfSteps; i++)
      {
        IEnumerable<Bar> singleDayBars = DataSourceManager.GetDataSource().GetFollowingBars(requestedDay);
        if(!singleDayBars.Any())
        {
          continue;
        }

          // update by one day to get the next bar.
          requestedDay = singleDayBars.First().Time.AddDays(1.0);

        Algorithm.HandleBarIncrement(singleDayBars, this, InstrumentManager);

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

    private void ExecuteOrders()
    {
      foreach (IOrder ticket in PendingOrders)
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
            execBar = ticket.BarOfExecution;
            execTime = SimTimes[1];
            price = execBar.HasBidAsk
                ? (ticket.Quantity > 0 ? execBar.Ask : execBar.Bid)
                : execBar.Close;
            break;

          case OrderType.openNextBar:
            execBar = ticket.BarOfExecution;
            execTime = SimTimes[0];
            price = execBar.HasBidAsk
                ? (ticket.Quantity > 0 ? execBar.Ask : execBar.Bid)
                : execBar.Open;
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
        var fillPrice = ticket.Type == OrderType.cash
            || ticket.Type == OrderType.optionExpiryClose
                ? price
                : FillModel(ticket, execBar, price);

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

        // pay for it, unless it's the end-of-sim order
        // same reasoning as for adjustment of position applies
        if (ticket.Type != OrderType.endOfSimFakeClose)
        {
          SimulatorPortfolioInfo.Cash = SimulatorPortfolioInfo.Cash
              - numberOfShares * fillPrice
              - commission;
        }

        // add log entry
        Transaction transaction = new Transaction()
        {
          Symbol = ticket.Instrument.Ticker,
          Type = ticket.Instrument.IsOption
                ? (ticket.Instrument.IsOptionPut ? TransactionType.OptionPut : TransactionType.OptionCall)
                : TransactionType.Equity,
          Order = ticket,
          BarOfExecution = execBar,
          FillPrice = fillPrice,
          Commission = commission
        };
        TransactionHistory.AddTransaction(transaction);
      }
    }
  }
}
