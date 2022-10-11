namespace TuringTraderWin
{
  public partial class MainWindow : Form
  {
    private IAlgorithmManager AlgorithmManager;
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
      AlgorithmComboBox.Items.AddRange(AlgorithmManager.AlgorithmList.ToArray());
    }
  }
}