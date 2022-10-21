using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.Simulator
{
  /// <summary>
  /// Holds the information about a specific simulation's portfolio.
  /// </summary>
  public class SimulatorPortfolioInfo : ISimulatorPortfolioInfo
  {
    public double Cash { get; set; }
    public double NetAssetValueHighestHigh { get; set; }
    public double NetAssetValueMaxDrawdown { get; set; }
    public double CommissionPerShare { get; set; }
    public double CashContributions { get; set; }
  }
}
