using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TuringTraderWin.Algorithm;
using TuringTraderWin.Optimizer;
using TuringTraderWin.Simulator;
using IAlgorithm = TuringTraderWin.Algorithm.IAlgorithm;

namespace TuringTraderWin
{
    public partial class MainWindow : Form
  {
    private readonly IAlgorithmManager AlgorithmManager;
    private readonly ILogger Logger;
    private readonly IOptimizerManager OptimizerManager;
    private readonly ISimulatorManager SimulatorManager;

    private readonly IServiceProvider ServiceProvider;

    public MainWindow(IAlgorithmManager algorithmManager, ILogger<MainWindow> logger, IOptimizerManager optimizerManager, ISimulatorManager simulatorManager, IServiceProvider serviceProvider)
    {
      Logger = logger;
      AlgorithmManager = algorithmManager;
      OptimizerManager = optimizerManager;
      SimulatorManager = simulatorManager;
      ServiceProvider = serviceProvider;
      InitializeComponent();

      UpdateAlgorithmDropDown();
    }
    

    private void UpdateAlgorithmDropDown()
    {
      AlgorithmManager.LoadAlgorithms();
      AlgorithmComboBox.Items.Clear();
      AlgorithmComboBox.Items.AddRange(AlgorithmManager.AlgorithmList.ToArray());
    }

    private void AlgorithmComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      AlgorithmManager.SelectedAlgorithm = (IAlgorithm)AlgorithmComboBox.SelectedItem;
      UpdateOptimizationParameters();

    }

    private void UpdateOptimizationParameters()
    {
      if (AlgorithmManager.SelectedAlgorithm == null)
      {

        return;
      }


      OptimizationGridView.Rows.Clear();
      foreach(AlgorithmParameter parameter in OptimizerManager.GetDefaultParams(AlgorithmManager.SelectedAlgorithm))
      {
        OptimizationGridView.Rows.Add(new object[] { parameter.Name, parameter.Value, parameter.IsEnabled, parameter.Start, parameter.End, parameter.IncrementStepAmount });

      }
    }

    private void RunButton_Click(object sender, EventArgs e)
    {
      ISimulatorCore sim = ServiceProvider.GetService<ISimulatorCore>();
      sim.Name = AlgorithmComboBox.Text;
      sim.Algorithm = AlgorithmManager.SelectedAlgorithm;
      // Update the sim algorithm parameters, or these should be set by the optimizerManager.
      // and we should just get them.
      SimulatorManager.AddSimulator(sim);
      SimulatorManager.RunSimulators(new List<string>() { sim.Name });
    }
    private IEnumerable<AlgorithmParameter> GetGridAlgorithmParameters()
    {
      foreach(DataGridViewRow row in OptimizationGridView.Rows)
      {
        
        row.Cells[0].Value
      }
    }
  }
}