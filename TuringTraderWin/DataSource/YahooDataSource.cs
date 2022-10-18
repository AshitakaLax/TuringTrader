using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;
using YahooFinanceApi;

namespace TuringTraderWin.DataSource
{
  public class YahooDataSource : IDataSource
  {

    //TODO: Utilize existing libraries to pull in dependency on Yahoo Finance. Also check whether the data already exists in a Cache
    // We will need to have a cache that exists prior to that.
    public List<Bar> CachedData { get; set; }

    public IEnumerable<Bar> LoadData(DateTime startTime, DateTime endTime)
    {
      List<Bar> data = new List<Bar>();
      IReadOnlyList<Candle> history = Yahoo.GetHistoricalAsync("SQQQ", startTime, endTime, Period.Daily).Result;
      foreach(Candle candle in history)
      {
        data.Add(new Bar("", candle.DateTime, (double)candle.Open, (double)candle.High, (double)candle.Low, (double)candle.Close, candle.Volume));
      }

      return data;
    }

    public void UpdateData(List<Bar> data, DateTime startTime, DateTime endTime)
    {
      throw new NotImplementedException();
    }
  }
}
