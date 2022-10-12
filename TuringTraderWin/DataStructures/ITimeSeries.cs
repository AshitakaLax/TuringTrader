using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.DataStructures
{    
  /// <summary>
     /// Interface for time series data. This interface provides access to
     /// a limited number of historical values.
     /// </summary>
     /// <typeparam name="T">type of time series</typeparam>
  public interface ITimeSeries<T>
  {
    /// <summary>
    /// Retrieve historical value from time series.
    /// </summary>
    /// <param name="barsBack">number of bars back, 0 for current bar</param>
    /// <returns>historical value</returns>
    T this[int barsBack]
    {
      get;
    }

  }
}
