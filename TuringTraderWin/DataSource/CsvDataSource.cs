using CsvHelper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;
using TuringTraderWin.Instruments;

namespace TuringTraderWin.DataSource
{
  public class CsvDataSource : IDataSource
  {
    /// <summary>
    /// The Logger.
    /// </summary>
    private readonly ILogger Logger;

    private readonly string StockCsvDataPath;

    public CsvDataSource(ILogger<CsvDataSource> logger, string filePath)
    {
      Logger = logger;
      StockCsvDataPath = filePath;
    }
    public List<Bar> CachedData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public ConcurrentDictionary<IInstrument, IEnumerable<Bar>> InstrumentDataCache { get; set; } = new ConcurrentDictionary<IInstrument, IEnumerable<Bar>>();
    public int Priority { get; set; } = -1;

    public bool CanSupportInstrument(IInstrument instrument)
    {
      if (instrument.IsOption || string.IsNullOrEmpty(instrument.Ticker))
      {
        return false;
      }

      // check whether we have access to the folder and ticker for that file.
      string csvFilePath = DoesCsvFileExist(instrument.Ticker);
      if (string.IsNullOrEmpty(csvFilePath))
      {
        return false;
      }

      //TODO: determine whether the time range in the csv covers the requested range.
      return true;

    }

    public IEnumerable<Bar> GetFollowingBars(DateTime time)
    {

      List<Bar> bars = new List<Bar>();
      foreach (KeyValuePair<IInstrument, IEnumerable<Bar>> pair in InstrumentDataCache)
      {
        Bar matchingBar = pair.Value.Where(b => b.Time >= time).FirstOrDefault();
        if (matchingBar != null)
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
      foreach (IEnumerable<Bar> bars in InstrumentDataCache.Values)
      {
        if (bars.Count() > largestNumber)
        {
          largestNumber = bars.Count();
        }
      }
      return largestNumber;
    }

    public IEnumerable<Bar> LoadData(string ticker, DateTime startTime, DateTime endTime)
    {
      string csvFilePath = DoesCsvFileExist(ticker);
      if (string.IsNullOrWhiteSpace(csvFilePath))
      {
        return null;
      }

      using var reader = new StreamReader(csvFilePath);
      using var csv = new CsvReader(reader);
      IEnumerable<CsvBar> records = csv.GetRecords<CsvBar>();
      Logger.LogInformation($"Read {records.Count()} records for ticker {ticker}.");
      return records.Select(bar => new Bar(bar.Name, bar.Date, bar.Open, bar.High, bar.Low, bar.Close, (long)bar.Volume));
    }

    public List<Bar> LoadData(IInstrument instrument, DateTime startTime, DateTime endTime)
    {
      InstrumentDataCache[instrument] = LoadData(instrument.Ticker, startTime, endTime);
      return InstrumentDataCache[instrument].ToList();
    }

    public void UpdateData(List<Bar> data, DateTime startTime, DateTime endTime)
    {// updates the data available with the latest data.
      //merge the two data stacks together.
    }

    /// <summary>
    /// Checks whether the specific CSV File exists.
    /// </summary>
    /// <param name="ticker">The ticker to fetch for the file.</param>
    /// <returns>Return the File path to the csv file, null if it doesn't exist.</returns>
    private string DoesCsvFileExist(string ticker)
    {
      //verify that there is a 
      if (!Directory.Exists(StockCsvDataPath))
      {
        Logger.LogWarning($"Could not find path for {StockCsvDataPath}");
        return null;
      }

      string csvFilePath = Path.Combine(StockCsvDataPath, $"{ticker}.csv");
      if (!File.Exists(csvFilePath))
      {
        csvFilePath = Path.Combine(StockCsvDataPath, $"{ticker}_data.csv");
      }
      else
      {
        return null;
      }

      if (!File.Exists(csvFilePath))
      {
        Logger.LogWarning($"Could not find {ticker}.csv or {ticker}_data.csv in {StockCsvDataPath}.");
        return null;
      }

      return csvFilePath;
    }
  }
}
