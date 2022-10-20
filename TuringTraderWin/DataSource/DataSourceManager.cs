using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTrader.Simulator;
using TuringTraderWin.Instruments;

namespace TuringTraderWin.DataSource
{
  public class DataSourceManager : IDataSourceManager
  {
    /// <summary>
    /// The Logger.
    /// </summary>
    private readonly ILogger Logger;


    public DataSourceManager(ILogger<DataSourceManager> logger, CsvDataSource csvDataSource = null)
    {
      Logger = logger;
      if(csvDataSource != null)
      {
        DataSources.Add(csvDataSource.Priority, csvDataSource);
      }

      IDataSource yahooSource = new YahooDataSource();
      DataSources.Add(yahooSource.Priority, yahooSource);
    }

    public SortedList<int, IDataSource> DataSources { get; set; } = new SortedList<int, IDataSource>();
    public ConcurrentDictionary<IInstrument, IDataSource> DataDictionary { get; set; } = new ConcurrentDictionary<IInstrument, IDataSource>();

    public void AddDataSource(IDataSource dataSource)
    {
      DataSources.Add(dataSource.Priority, dataSource);
    }

    public void AddDataSource(string ticker)
    {

    }

    public IDataSource GetDataSource()
    {
      return DataSources.Values[0];
    }

    public long GetNumberOfRunSteps()
    {
      long steps = 0;
      foreach (KeyValuePair<int, IDataSource> pair in DataSources)
      {
        long dataSourceSteps = pair.Value.GetNumberOfTimeSteps();
        if(dataSourceSteps > steps)
        {
          steps = dataSourceSteps;
        }
      }

      return steps;
    }

    public void LoadDataSources(IEnumerable<IInstrument> instruments, DateTime start, DateTime stop)
    {

      Parallel.ForEach(instruments, (instrument) =>
      {
        bool instrumentIsSupported = false;
        foreach (KeyValuePair<int, IDataSource> pair in DataSources)
        {
          IDataSource dataSource = pair.Value;
          if (dataSource.CanSupportInstrument(instrument) && !instrumentIsSupported)
          {
            instrumentIsSupported = true;
            DataDictionary[instrument] = dataSource;
            dataSource.LoadData(instrument, start, stop);
            break;
          }
        }
        if(!instrumentIsSupported)
        {
          Logger.LogWarning($"Instrument {instrument.Name}, doesn't have a supported DataSource.");
        }
      });

      //foreach (IInstrument instrument in instruments)
      //{
      //  foreach(IDataSource dataSource in DataSources)
      //  {
      //    if(dataSource.CanSupportInstrument(instrument))
      //    {
      //      DataDictionary[instrument] = dataSource;
      //      dataSource.LoadData(instrument, start, stop);
      //      // TODO load up the Data Sources for the time range of the simulation here, or whatever is in the cache.
      //      break;

      //    }
      //  }
      //}
    }
  }
}
