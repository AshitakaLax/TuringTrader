using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TuringTrader.Simulator;

namespace TuringTraderWin
{
  /// <summary>
  /// This handles the loading of all the different Algorithms
  /// </summary>
  public class AlgorithmManager
  {
    public AlgorithmManager()
    {

    }

    public List<AlgorithmInfo> AlgorithmInfoList { get; set; } = new List<AlgorithmInfo>();
    public void LoadAlgorithms()
    {
      AlgorithmInfoList = TuringTrader.Simulator.AlgorithmLoader.GetAllAlgorithms();
    }
  }
}
