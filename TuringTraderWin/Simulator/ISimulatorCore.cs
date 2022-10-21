using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.Algorithm;
using TuringTraderWin.DataStructures;
using TuringTraderWin.Orders;

namespace TuringTraderWin.Simulator
{
  /// <summary>
  /// It simulator core iterface.
  /// </summary>
  public interface ISimulatorCore
  {
    /// <summary>
    /// Total net liquidation value of all positions plus cash.
    /// </summary>
    TimeSeries<double> NetAssetValue { get; set; }

    ISimulatorPortfolioInfo SimulatorPortfolioInfo { get; set; }

    /// <summary>
    /// Return class type name. This method will return the name of the
    /// derived class, typically a proprietary algorithm derived from
    /// Algorithm.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Gets or sets the current location you are in time.
    /// </summary>
    long CurrentTradingBar { get; set; }

    DateTime StartTime { get; set; }
    DateTime? WarmupStartTime { get; set; }

    DateTime EndTime { get; set; }

    int TradingDays { get; set; }

    TimeSeries<DateTime> SimTimes { get; set; }

    /// <summary>
    /// Gets or sets the Algorithm associated with the Simulation.
    /// </summary>
    IAlgorithm  Algorithm { get; set; }

    /// <summary>
    /// Gets or sets the Parameters for the specific Algorithm.
    /// </summary>
    IEnumerable<AlgorithmParameter> AlgorithmParameters { get; set; }

    /// <summary>
    /// Flag, indicating the last bar processed by the simulator. Algorithms
    /// may use this to implement special handling of this last bar, e.g.
    /// setting up live trades.
    /// </summary>
    bool IsLastBar { get; set; }

    /// <summary>
    /// List of pending orders.
    /// </summary>
    List<Order> PendingOrders { get; set; }

    // Positions is apart of the IInstrumentsManager now.

    /// <summary>
    /// Runs the simulator on its own thread.
    /// The SimulatorManager will manage running and stopping this simulator.
    /// </summary>
    /// <param name="cancellationToken">The token to know whether the Simulation has been cancelled.</param>
    void RunSimulator(CancellationToken cancellationToken);

    void InitializeSimTimes();

    /// <summary>
    /// Calculates and returns the Net AssetValue.
    /// Similar
    /// </summary>
    /// <returns>The Algorithm's Net Asset Value.</returns>
    double GetNetAssetValue();

    /// <summary>
    /// Deposit cash into account. Note that the deposit amount
    /// must be positive.
    /// </summary>
    /// <param name="amount">amount to deposit</param>
    void Deposit(double amount);

    /// <summary>
    /// Withdraw cash from account. Note that the withdrawal
    /// amount must be positive.
    /// </summary>
    /// <param name="amount">amount to withdraw</param>
    void Withdraw(double amount);

    /// <summary>
    /// Queue's an order with the simulator.
    /// </summary>
    /// <param name="order">The order to execute.</param>
    void QueueOrder(Order order);

    /// <summary>
    /// Order fill model. This method is only called for those orders
    /// which are executed, but not for those which expired. The default
    /// implementation fills orders at their theoretical price. Algorithms
    /// can override this method to implement more realistic fill models
    /// reflecting slippage.
    /// </summary>
    /// <param name="orderTicket">original order ticket</param>
    /// <param name="barOfExecution">bar of order execution</param>
    /// <param name="theoreticalPrice">theoretical fill price</param>
    /// <returns>custom fill price. default: theoretical fill price</returns>
    double FillModel(Order orderTicket, Bar barOfExecution, double theoreticalPrice);


    /// <summary>
    /// Validate simulator timestamp. Timestamps deemed invalid will be
    /// skipped and not passed on to the user algorithm. The default 
    /// implementation is geared at the U.S. stock market and skips 
    /// all bars not within regular trading hours of the NYSE.
    /// </summary>
    /// <param name="timestamp">simulator timestamp</param>
    /// <returns>true, if valid</returns>
    bool IsValidSimTime(DateTime timestamp);


    /// <summary>
    /// Determine next sim time. This hook is used by the simulator to
    /// determine the value for NextSimTime, after reaching the end of 
    /// the available historical bars. The default implementation assumes
    /// the trading calendar for U.S. stock exchanges.
    /// </summary>
    /// <param name="timestamp"></param>
    /// <returns>next simulator timestamp</returns>
    DateTime CalcNextSimTime(DateTime timestamp);


    /// <summary>
    /// Validate bar. This hook is used by the simulator to validate
    /// bars. Invalid bars are relevant in the following situations:
    /// (1) option chain: Simulator.OptionChain will not return contracts
    /// with invalid bars at the current sim time. 
    /// (2) nav calculation: The simulator will ignore invalid bars when
    /// calculating Simulator.NetAssetValue
    /// The default implementation marks bars invalid under the following 
    /// conditions:
    /// (1) bid volume or ask volume is zero.
    /// (2) bid price is less than 20% of ask price.
    /// </summary>
    /// <param name="bar"></param>
    /// <returns></returns>
    bool IsValidBar(Bar bar);

    // TODO-LB: Add support for List<IInstrument> OptionChain(DataSource ds)
  }
}
