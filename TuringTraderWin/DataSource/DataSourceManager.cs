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


    public DataSourceManager(ILogger<DataSourceManager> logger)
    {
      Logger = logger;
      DataSources.Add(new YahooDataSource());
    }

    public List<IDataSource> DataSources { get; set; } = new List<IDataSource>();
    public ConcurrentDictionary<IInstrument, IDataSource> DataDictionary { get; set; } = new ConcurrentDictionary<IInstrument, IDataSource>();

    public void AddDataSource(IDataSource dataSource)
    {
      DataSources.Add(dataSource);
    }

    public void AddDataSource(string ticker)
    {

    }

    public IDataSource GetDataSource()
    {
      return DataSources.First();
    }

    public void LoadDataSources(IEnumerable<IInstrument> instruments)
    {
      foreach (IInstrument instrument in instruments)
      {
        foreach(IDataSource dataSource in DataSources)
        {
          if(dataSource.CanSupportInstrument(instrument))
          {
            DataDictionary[instrument] = dataSource;
            break;

          }
        }
      }
    }
  }
}
