using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;
using TuringTraderWin.Instruments;

namespace TuringTraderWin.DataSource
{
  /// <summary>
  /// The Data Source.
  /// </summary>
  public interface IDataSource
  {
    //TODO: Create Priority with the data sources so we could add multiple Data Sources(such as a local CSV file).

    /// <summary>
    /// Load data between time stamps into memory.
    /// </summary>
    /// <param name="ticker">The ticker.</param>
    /// <param name="startTime">beginning time stamp</param>
    /// <param name="endTime">end time stamp</param>
    IEnumerable<Bar> LoadData(string ticker, DateTime startTime, DateTime endTime);

    /// <summary>
    /// Gets the greatest number of steps for the data available.
    /// </summary>
    /// <returns>The number of steps.</returns>
    long GetNumberOfTimeSteps();

    /// <summary>
    /// Loads the data to be local.
    /// </summary>
    /// <param name="instrument">The data to load.</param>
    /// <param name="startTime">beginning time stamp</param>
    /// <param name="endTime">end time stamp</param>
    void LoadData(IInstrument instrument, DateTime startTime, DateTime endTime);

    void UpdateData(List<Bar> data, DateTime startTime, DateTime endTime);

    /// <summary>
    /// This will get a list of all of the bar for each instrument.
    /// So it will only return 1 bar for each instrament.
    /// </summary>
    /// <param name="time">The time that equal to the bar or the first matching one that follows.</param>
    /// <returns>List of the first bar for each instrument following the time stamp.</returns>
    IEnumerable<Bar> GetFollowingBars(DateTime time);

    /// <summary>
    /// Gets whether the DataSource can provide support for the Instrument.
    /// </summary>
    /// <param name="instrument">The instrament to check.</param>
    /// <returns>True if it can support, fales otherwise.</returns>
    bool CanSupportInstrument(IInstrument instrument);

    /// <summary>
    /// Data sources using the cache make their data accessible here.
    /// This may be used for algorithms and report generators, e.g.,
    /// MFE/MAE analysis, which requires access to the data independent
    /// of the simulator's current bar.
    /// </summary>
    public List<Bar> CachedData { get; set; }
  }
}
