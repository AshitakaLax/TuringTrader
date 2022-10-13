using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public SimulatorCore(ILogger<SimulatorCore> logger, IDataSourceManager dataSourceManager, IInstrumentManager instrumentManager, IOptimizerManager optimizerManager)
    {
      Logger = logger;
      DataSourceManager = dataSourceManager;
      InstrumentManager = instrumentManager;
      OptimizerManager = optimizerManager;
    }

    public TimeSeries<double> NetAssetValue { get; set; }
    public ISimulatorPortfolioInfo simulatorPortfolioInfo { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? WarmupStartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int TradingDays { get; set; }
    public IEnumerable<DateTime> SimTimes { get; set; }
    public bool IsLastBar { get; set; }
    public List<Order> PendingOrders { get; set; }
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
      throw new NotImplementedException();
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
      throw new NotImplementedException();
    }

    public void RunSimulator(CancellationToken cancellationToken)
    {
      //
    }

    public void Withdraw(double amount)
    {
      throw new NotImplementedException();
    }
  }
}
