using Microsoft.Extensions.Logging;
using TuringTrader.Simulator;
using TuringTraderWin.Optimizer;

namespace TuringTraderWin
{
  public partial class MainWindow : Form
  {
    private readonly IAlgorithmManager AlgorithmManager;
    private readonly ILogger Logger;
    private readonly IOptimizerManager OptimizerManager;

    public MainWindow(IAlgorithmManager algorithmManager, ILogger<MainWindow> logger, IOptimizerManager optimizerManager)
    {
      Logger = logger;
      AlgorithmManager = algorithmManager;
      OptimizerManager = optimizerManager;
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
      foreach(OptimizerParam parameter in OptimizerManager.GetParams(AlgorithmManager.SelectedAlgorithm))
      {
        OptimizationGridView.Rows.Add(new object[] { parameter.Name, parameter.Value, parameter.IsEnabled, parameter.Start, parameter.End, parameter.Step });

      }
    }
  }
}