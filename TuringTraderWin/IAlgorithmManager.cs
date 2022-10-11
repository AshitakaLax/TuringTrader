using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTrader.Simulator;

namespace TuringTraderWin
{
  /// <summary>
  /// The Iterface for the Algorithm Manager.
  /// </summary>
  public interface IAlgorithmManager
  {
    List<IAlgorithm> AlgorithmList { get; set; }

    /// <summary>
    /// Reloads the Algorithms.
    /// </summary>
    public void LoadAlgorithms();

  }
}
