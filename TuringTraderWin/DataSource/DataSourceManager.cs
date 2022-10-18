using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTrader.Simulator;

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

    public void AddDataSource(IDataSource dataSource)
    {
      DataSources.Add(dataSource);
    }

    public void AddDataSource(string ticker)
    {
      throw new NotImplementedException();
    }

    public IDataSource GetDataSource()
    {
      return DataSources.First();
    }
  }
}
