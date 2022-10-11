namespace TuringTraderWin
{
  public partial class MainWindow : Form
  {
    private AlgorithmManager AlgorithmManager;
    public MainWindow()
    {
      InitializeComponent();
      AlgorithmManager = new AlgorithmManager();
      UpdateAlgorithmDropDown();
    }
    

    private void UpdateAlgorithmDropDown()
    {
      AlgorithmManager.LoadAlgorithms();
      AlgorithmComboBox.Items.Clear();
      AlgorithmComboBox.Items.AddRange(AlgorithmManager.AlgorithmInfoList.Select(algo => algo.Name).Cast<object>().ToArray());
    }
  }
}