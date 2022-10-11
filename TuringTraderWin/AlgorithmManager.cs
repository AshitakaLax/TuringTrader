using AshitakalaxAlgos;
using TuringTrader.Simulator;

namespace TuringTraderWin
{
  /// <summary>
  /// This handles the loading of all the different Algorithms
  /// </summary>
  public class AlgorithmManager: IAlgorithmManager
  {
    /// <summary>
    /// Creates the Algorthim Manager.
    /// </summary>
    public AlgorithmManager()
    {

    }

    /// <summary>
    /// Gets or sets the algorithms available to run.
    /// </summary>
    public List<IAlgorithm> AlgorithmList { get; set; } = new List<IAlgorithm>();

    /// <inheritdoc />
    public void LoadAlgorithms()
    {
      //We can enhance this, but the idea is to have a simple creation of the algo's.
      AlgorithmList.Add(new SimpleSMA());
    }
  }
}
