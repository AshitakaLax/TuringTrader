using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;
using TuringTraderWin.Instruments;

namespace TuringTraderWin.DataSource
{
  /// <summary>
  /// Manages the creation of various Data Sources.
  /// </summary>
  public interface IDataSourceManager
  {
    ConcurrentDictionary<IInstrument, List<Bar>> Data { get; set; }

    /// <summary>
    /// Gets the Data Source.
    /// </summary>
    /// <returns></returns>
    IDataSource GetDataSource();

    /// <summary>
    /// Goes through all of the data sources to determine the number of steps available for the simulation to run.
    /// </summary>
    /// <returns>The number of steps/bars in a simulation.</returns>
    long GetNumberOfRunSteps();

    /// <summary>
    /// Loads data from the sources to 
    /// </summary>
    /// <param name="instruments">The instruments to load.</param>
    /// <param name="start">The Start Time of the simulation.</param>
    /// <param name="stop">The stop time of the simualation.</param>
    void LoadDataSources(IEnumerable<IInstrument> instruments, DateTime start, DateTime stop);

    /// <summary>
    /// Adds a Data Source to the Data Source Manager.
    /// </summary>
    /// <param name="dataSource"></param>
    void AddDataSource(IDataSource dataSource);

    /// <summary>
    /// Adds a Data Source to the Data Source Manager.
    /// </summary>
    /// <param name="ticker">The stock Ticker to get data about.</param>
    void AddDataSource(string ticker);

  }
}
