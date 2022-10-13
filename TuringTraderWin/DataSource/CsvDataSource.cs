using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;

namespace TuringTraderWin.DataSource
{
  public class CsvDataSource : IDataSource
  {
    /// <summary>
    /// The Logger.
    /// </summary>
    private readonly ILogger Logger;

    public CsvDataSource(ILogger<CsvDataSource> logger, string filePath)
    {
      Logger = logger;
    }
    public List<Bar> CachedData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IEnumerable<Bar> LoadData(DateTime startTime, DateTime endTime)
    {
      // TODO utilize CsvHelper to create the data needed.
      return null;
    }

    public void UpdateData(List<Bar> data, DateTime startTime, DateTime endTime)
    {// updates the data available with the latest data.
      //merge the two data stacks together.
    }
  }
}
