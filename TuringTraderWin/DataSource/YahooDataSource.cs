using NodaTime;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;
using TuringTraderWin.Instruments;
//using YahooFinanceApi;
using YahooQuotesApi;
using Security = YahooQuotesApi.Security;

namespace TuringTraderWin.DataSource
{
  public class YahooDataSource : IDataSource
  {
    private YahooQuotes Quotes;

    public YahooDataSource()
    {
      Quotes = new YahooQuotesBuilder().Build();
    }

    //TODO: Utilize existing libraries to pull in dependency on Yahoo Finance. Also check whether the data already exists in a Cache
    // We will need to have a cache that exists prior to that.
    public List<Bar> CachedData { get; set; }
    public ConcurrentDictionary<IInstrument, IEnumerable<Bar>> InstrumentDataCache { get; set; } = new ConcurrentDictionary<IInstrument, IEnumerable<Bar>>();

    public bool CanSupportInstrument(IInstrument instrument)
    {
      if(instrument.IsOption || string.IsNullOrEmpty(instrument.Ticker))
      {
        return false;
      }

      return true;
    }

    public IEnumerable<Bar> GetFollowingBars(DateTime time)
    {
      List<Bar> bars = new List<Bar>();
      foreach(KeyValuePair<IInstrument, IEnumerable<Bar>> pair in InstrumentDataCache)
      {
        Bar matchingBar = pair.Value.Where(b => b.Time >= time).FirstOrDefault();
        if(matchingBar != null)
        {
          bars.Add(matchingBar);
        }
      }
      return bars;
    }

    public long GetNumberOfTimeSteps()
    {
      // currently based on a time interval of 1 day.
      // TODO: Update to handle differing start times review for overlap
      // Currently it will only iterato over the lowest number available.
      long largestNumber = 0;
      foreach(IEnumerable<Bar> bars in InstrumentDataCache.Values)
      {
        if(bars.Count() > largestNumber)
        {
          largestNumber = bars.Count();
        }
      }
      return largestNumber;
    }

    public IEnumerable<Bar> LoadData(string ticker, DateTime startTime, DateTime endTime)
    {
      List<Bar> data = new List<Bar>();
      Quotes = new YahooQuotesBuilder().WithHistoryStartDate(Instant.FromDateTimeUtc(startTime.ToUniversalTime())).Build();
     Security security = Quotes.GetAsync(ticker, HistoryFlags.PriceHistory).Result;

      PriceTick[] ticks = security.PriceHistory.Value;
      foreach(PriceTick candle in ticks)
      {
        data.Add(new Bar(ticker, candle.Date.ToDateTimeUnspecified(), (double)candle.Open, (double)candle.High, (double)candle.Low, (double)candle.Close, candle.Volume));
      }

      return data;
    }

    public void LoadData(IInstrument instrument, DateTime startTime, DateTime endTime)
    {
      InstrumentDataCache[instrument] = LoadData(instrument.Ticker, startTime, endTime);
    }

    public void UpdateData(List<Bar> data, DateTime startTime, DateTime endTime)
    {
      throw new NotImplementedException();
    }
  }
}
