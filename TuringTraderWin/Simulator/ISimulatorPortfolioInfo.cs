using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.Simulator
{
  public interface ISimulatorPortfolioInfo
  {
    /// <summary>
    /// Gets or sets the initial funds you are starting with in your portfolio.
    /// </summary>
    double CashContributions { get; set; }

    /// <summary>
    /// Currently available cash position. Algorithms will typically
    /// initialize this value at the beginning of the simulation.
    /// </summary>
    double Cash { get; set; }

    /// <summary>
    /// Highest high of net asset value.
    /// </summary>
    double NetAssetValueHighestHigh { get; set; }

    /// <summary>
    /// Maximum drawdown of net asset value, expressed
    /// as a fractional value between 0 and 1.
    /// </summary>
    double NetAssetValueMaxDrawdown { get; set; }

    /// <summary>
    /// Commision to be paid per share. The default value is zero, equivalent
    /// to no commissions. Algorithms should set this to match the commissions
    /// paid on high account values/ large numbers of shares traded.
    /// </summary>
    double CommissionPerShare { get; set; }
  }
}
