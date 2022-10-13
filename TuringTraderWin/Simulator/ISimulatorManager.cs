using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.Simulator
{
  /// <summary>
  /// The Manager for the simulations.
  /// </summary>
  public interface ISimulatorManager
  {
    /// <summary>
    /// Adds a The Simulator to the manager.
    /// </summary>
    /// <param name="simulator"></param>
    void AddSimulator(ISimulatorCore simulator);

    /// <summary>
    /// Starts all of simulations that have been added by the name of the algorithm.
    /// </summary>
    /// <param name="simulatorNames">The name of the algorithms to start.</param>
    void RunSimulators(IEnumerable<string> simulatorNames);

    /// <summary>
    /// Stops the simulation at the complete of its next bar/iteration.
    /// </summary>
    /// <param name="simulatorNames">The Name of the algorithm associated with the SimulatorCore.</param>
    void StopSimulators(IEnumerable<string> simulatorNames);

    /// <summary>
    /// Sets the Progress Handler for the Simulator.
    /// </summary>
    /// <param name="simulatorProgressCallback"></param>
    void SetSimulatorProgressHandler(Action<string, int> simulatorProgressCallback);
    void SetSimulatorCompleteHandler(Action<ISimulatorPortfolioInfo,ISimulatorCore> handler);
  }
}
