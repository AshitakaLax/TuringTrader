using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.Algorithm;
using TuringTraderWin.DataSource;

namespace TuringTraderWin.Simulator
{
  public class SimulatorManager : ISimulatorManager
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
    /// The SimulatorComplete callback handler.
    /// </summary>
    private Action<ISimulatorPortfolioInfo, ISimulatorCore> SimCompleteHandler;

    private Action<string, int> SimulatorProgressCallback;

    /// <summary>
    /// The Simulator Manager that handles running all of the Simulators.
    /// </summary>
    /// <param name="logger">The Logger.</param>
    public SimulatorManager(ILogger<SimulatorManager> logger, IDataSourceManager dataSourceManager)
    {
      Logger = logger;
    }
    public ConcurrentDictionary<string, ISimulatorCore> Simulations { get; set; } = new ConcurrentDictionary<string, ISimulatorCore>();
    
    public void AddSimulator(ISimulatorCore simulator)
    {
      if (Simulations.ContainsKey(simulator.Name))
      {
        throw new InvalidOperationException("Can't add existing Simulator.");
      }

      Simulations[simulator.Name] = simulator;
    }

    public void RunSimulators(IEnumerable<string> simulatorNames)
    {
      // Enhancement would be to have the Simulator State as part of the ISimulatorCore
      IEnumerable<ISimulatorCore> simulators = Simulations.Where(simPair => simulatorNames.Contains(simPair.Key)).Select(pair => pair.Value);
      ConcurrentDictionary<ISimulatorCore, CancellationTokenSource> cancellationTokens = new ConcurrentDictionary<ISimulatorCore, CancellationTokenSource>();
      // Initialize all of the Simulators
      Parallel.ForEach(simulators, sim =>
      {
        ConcurrentDictionary<string, AlgorithmParameter> algorithmParameters = new ConcurrentDictionary<string, AlgorithmParameter>(sim.AlgorithmParameters.ToDictionary(param => param.Name));
        sim.Algorithm.Initialize(algorithmParameters, DataSourceManager);

        cancellationTokens[sim] = new CancellationTokenSource();

      });

      // Start all of the Simulators on separate Threads and 
      Parallel.ForEach(simulators, sim =>
      {
        Task simTask = Task.Run(() =>
        {
          sim.RunSimulator(cancellationTokens[sim].Token);
        }, cancellationTokens[sim].Token);        
      });
    }

    public void SetSimulatorCompleteHandler(Action<ISimulatorPortfolioInfo, ISimulatorCore> handler)
    {
      SimCompleteHandler = handler;
    }

    public void SetSimulatorProgressHandler(Action<string, int> simulatorProgressCallback)
    {
      SimulatorProgressCallback = simulatorProgressCallback;
    }

    public void StopSimulators(IEnumerable<string> simulatorNames)
    {

    }
  }
}
