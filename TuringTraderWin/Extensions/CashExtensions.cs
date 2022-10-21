using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.Extensions
{
  public static class CashExtensions
  {
    /// <summary>
    /// The single location to format all the locations we want to print the cash to a string.
    /// </summary>
    /// <param name="cash">the amount of money.</param>
    /// <returns>the formatted string.</returns>
    public static string PrintCash(this double cash)
    {
      return cash.ToString("C", CultureInfo.CurrentCulture);
    }
  }
}
