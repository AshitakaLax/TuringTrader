using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;

namespace TuringTraderWin.DataSource
{
  /// <summary>
  /// The Data Source.
  /// </summary>
  public interface IDataSource
  {
    /// <summary>
    /// Load data between time stamps into memory.
    /// </summary>
    /// <param name="startTime">beginning time stamp</param>
    /// <param name="endTime">end time stamp</param>
    IEnumerable<Bar> LoadData(DateTime startTime, DateTime endTime);

    void UpdateData(List<Bar> data, DateTime startTime, DateTime endTime);


    /// <summary>
    /// Data sources using the cache make their data accessible here.
    /// This may be used for algorithms and report generators, e.g.,
    /// MFE/MAE analysis, which requires access to the data independent
    /// of the simulator's current bar.
    /// </summary>
    public List<Bar> CachedData { get; set; }
  }
}
