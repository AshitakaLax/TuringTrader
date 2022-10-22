using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.Algorithm;
using TuringTraderWin.DataSource;
using TuringTraderWin.Instruments;
using TuringTraderWin.Optimizer;
using TuringTraderWin.Orders;

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
    private readonly IOptimizerManager OptimizerManager;


    /// <summary>
    /// The Simulator Manager that handles running all of the Simulators.
    /// </summary>
    /// <param name="logger">The Logger.</param>
    public SimulatorManager(ILogger<SimulatorManager> logger, IDataSourceManager dataSourceManager, IOptimizerManager optimizerManager)
    {
      Logger = logger;
      DataSourceManager = dataSourceManager;
      OptimizerManager = optimizerManager;
    }
    public ConcurrentDictionary<string, ISimulatorCore> Simulations { get; set; } = new ConcurrentDictionary<string, ISimulatorCore>();
    
    public void AddSimulator(ISimulatorCore simulator)
    {
      if (Simulations.ContainsKey(simulator.Name))
      {
        return;
        //throw new InvalidOperationException("Can't add existing Simulator.");
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
        sim.SimulatorPortfolioInfo = new SimulatorPortfolioInfo();
        ConcurrentDictionary<string, AlgorithmParameter> algorithmParameters = new ConcurrentDictionary<string, AlgorithmParameter>(sim.AlgorithmParameters.ToDictionary(param => param.Name));
        IEnumerable<IInstrument> instruments = sim.Algorithm.Initialize(algorithmParameters, DataSourceManager, sim);
        DataSourceManager.LoadDataSources(instruments, sim.StartTime, sim.EndTime);
        
        cancellationTokens[sim] = new CancellationTokenSource();
      });

      ConcurrentDictionary<ISimulatorCore, Task> simTasks = new ConcurrentDictionary<ISimulatorCore, Task>();
      // Start all of the Simulators on separate Threads and 
      Parallel.ForEach(simulators, sim =>
      {
        Task simTask = Task.Run(() =>
        {
          sim.RunSimulator(cancellationTokens[sim].Token);
        }, cancellationTokens[sim].Token);
        simTasks[sim] = simTask;
      });

      foreach(Task simTask in simTasks.Values)
      {
        // TODO update to publish results to a specific UI location as results come in.
        simTask.Wait();
      }

      string allSimulations = string.Join(Environment.NewLine, simulators.Select(sim => sim.GenerateSimulatorReport()));

      File.WriteAllText("Results.txt", allSimulations);
      MessageBox.Show(allSimulations);
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
