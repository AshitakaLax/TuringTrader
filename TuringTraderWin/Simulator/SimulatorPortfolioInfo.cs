using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.Simulator
{
  public class SimulatorPortfolioInfo : ISimulatorPortfolioInfo
  {
    public double Cash { get; set; }
    public double NetAssetValueHighestHigh { get; set; }
    public double NetAssetValueMaxDrawdown { get; set; }
    public double CommissionPerShare { get; set; }
  }
}
