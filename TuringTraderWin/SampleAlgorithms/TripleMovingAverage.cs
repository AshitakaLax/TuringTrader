using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.Algorithm;
using TuringTraderWin.DataSource;
using TuringTraderWin.DataStructures;
using TuringTraderWin.Extensions;
using TuringTraderWin.Indicators;
using TuringTraderWin.Instruments;
using TuringTraderWin.Simulator;

namespace TuringTraderWin.SampleAlgorithms
{
  public class TripleMovingAverage : IAlgorithm
  {
    public IAlgorithmInfo Info => new AlgorithmInfo()
    {
      Name = "Simple Moving Average",
      Description = "Utilizing 3 different SMA and utilizing their slopes."
    };
    public IEnumerable<AlgorithmParameter> GetDefaultParameters => new List<AlgorithmParameter>()
    {
      new AlgorithmParameter()
      {
        Name = "FAST",
        Start = 10,
        End = 30,
        Value = 20,
        IncrementStepAmount = 5,
        Description = "The Fast Leg"
      },
      new AlgorithmParameter()
      {
        Name = "MEDIUM",
        Start = 30,
        End = 70,
        Value = 50,
        IncrementStepAmount = 5,
        Description = "The Medium Leg"
      },
      new AlgorithmParameter()
      {
        Name = "SLOW",
        Start = 80,
        End = 120,
        Value = 100,
        IncrementStepAmount = 5,
        Description = "The Slow Leg"
      }
    };
    
    /// <summary>
    /// The Progress Handler Callback.
    /// </summary>
    private Action<int> ProgressHandlerCallback;

    public IEnumerable<IInstrument> Initialize(ConcurrentDictionary<string, AlgorithmParameter> algorithmParameters, IDataSourceManager dataSourceManager, ISimulatorCore simulatorCore)
    {
      simulatorCore.Name = "Triple Moving Average";
      //Setup the Start and End times
      simulatorCore.StartTime = DateTime.Parse("10/18/2022", CultureInfo.InvariantCulture);
      simulatorCore.EndTime = DateTime.Parse("10/21/2022", CultureInfo.InvariantCulture);

      // Setup the initial Deposit
      simulatorCore.Deposit(30000);

      List<IInstrument> instruments = new List<IInstrument>();
      instruments.Add(new Instrument()
      {
        Name = "SQQQ",
        BackingSymbol = "SQQQ",
        IsOption = false,
        IsOptionPut = false,
        NickName = "SQQQ",
        Ticker = "SQQQ",
        Description = "The Leveraged inverse 3x NASDAQ"
      });
      instruments.Add(new Instrument()
      {
        Name = "TQQQ",
        BackingSymbol = "TQQQ",
        IsOption = false,
        IsOptionPut = false,
        NickName = "TQQQ",
        Ticker = "TQQQ",
        Description = "The leveraged 3x NASDAQ"
      });

      // These instruments data will be loaded into the datamanager later.
      return instruments;
    }

    public ConcurrentDictionary<IInstrument, List<double>> FastSMA { get; set; } = new ConcurrentDictionary<IInstrument, List<double>>();
    public ConcurrentDictionary<IInstrument, List<double>> MediumSMA { get; set; } = new ConcurrentDictionary<IInstrument, List<double>>();
    public ConcurrentDictionary<IInstrument, List<double>> SlowSMA { get; set; } = new ConcurrentDictionary<IInstrument, List<double>>();

    private bool BearBuilding = false;
    private bool BullBuilding = false;
    // TODO: Determine whether it would be better to have a Initialize indicators(which we could also do in parallel).
    //public void HandleBarIncrement(ConcurrentDictionary<string, Bar> currentBars, ISimulatorCore simulatorCore, IInstrumentManager instrumentManager)
    public void HandleBarIncrement(ConcurrentDictionary<IInstrument, List<Bar>> data, int index, ISimulatorCore simulatorCore, IInstrumentManager instrumentManager)
    {
      // calculate the Simple moving average value for this interation.
      // Handle Bear growth
      IInstrument bearInstrument = data.GetInstrumentByTicker("SQQQ");
      InitializeSMAInstruments(data, simulatorCore.AlgorithmParameters, bearInstrument);
      Bar currentBearBar = data[bearInstrument][index];
      int currentPosition = instrumentManager.Positions.ContainsKey(bearInstrument) ? instrumentManager.Positions[bearInstrument] : 0;
      try
      {
        HandleBuyOrSellInstruments(simulatorCore, bearInstrument, index, currentBearBar, currentPosition, ref BearBuilding);
      }
      catch(Exception ex)
      {
        throw ex;
      }

      // Handle Bull growth.
      IInstrument bullInstrument = data.GetInstrumentByTicker("TQQQ");
      InitializeSMAInstruments(data, simulatorCore.AlgorithmParameters, bullInstrument);
      Bar currentBullBar = data[bullInstrument][index];

       currentPosition = instrumentManager.Positions.ContainsKey(bullInstrument) ? instrumentManager.Positions[bullInstrument] : 0;
      try { 
      HandleBuyOrSellInstruments(simulatorCore, bullInstrument, index, currentBullBar, currentPosition, ref BullBuilding);
    }
      catch(Exception ex)
      {
        throw ex;
      }
}

    private void HandleBuyOrSellInstruments(ISimulatorCore simulatorCore, IInstrument instrument, int index, Bar currentBar, int currentPosition, ref bool isSlopeBuilding)
    {
      List<double> fastSMA = FastSMA[instrument];
      List<double> mediumSMA = MediumSMA[instrument];
      List<double> slowSMA = SlowSMA[instrument];
      int fastPeriod = simulatorCore.AlgorithmParameters.FirstOrDefault(ap => ap.Name == "FAST").Value;
      int mediumPeriod = simulatorCore.AlgorithmParameters.FirstOrDefault(ap => ap.Name == "MEDIUM").Value;
      int slowPeriod = simulatorCore.AlgorithmParameters.FirstOrDefault(ap => ap.Name == "SLOW").Value;

      if (index - slowPeriod < slowSMA.Count && index - slowPeriod - 1 > 0)
      {
        bool fastSlopeIsPositive = fastSMA[index - fastPeriod] > fastSMA[index - fastPeriod - 1];
        bool mediumSlopeIsPositive = mediumSMA[index - mediumPeriod] > mediumSMA[index - mediumPeriod - 1];
        bool slowSlopeIsPositive = slowSMA[index - slowPeriod] > slowSMA[index - slowPeriod - 1];

        if (!isSlopeBuilding && fastSlopeIsPositive && mediumSlopeIsPositive && slowSlopeIsPositive && currentPosition == 0)
        {
          isSlopeBuilding = true;
          this.Buy(instrument, simulatorCore, currentBar);
        }

        if (isSlopeBuilding && !fastSlopeIsPositive && currentPosition > 0)
        {
          isSlopeBuilding = false;
          this.Sell(instrument, simulatorCore, currentBar);
        }
      }
    }


    public void Run(ConcurrentDictionary<string, AlgorithmParameter> algorithmParameters)
    {

      if (ProgressHandlerCallback != null)
        ProgressHandlerCallback(100);
    }

    public void SetProgressHandler(Action<int> progressHandler)
    {
      ProgressHandlerCallback = progressHandler;
    }

    private void InitializeSMAInstruments(ConcurrentDictionary<IInstrument, List<Bar>> data, IEnumerable<AlgorithmParameter> parameters, IInstrument instrument)
    {
      if (!FastSMA.ContainsKey(instrument))
      {
        AlgorithmParameter fastParameter = parameters.FirstOrDefault(ap => ap.Name == "FAST");

        FastSMA[instrument] = data[instrument].SMA(fastParameter.Value, Ohlc.Close).ToList();
      }

      if (!MediumSMA.ContainsKey(instrument))
      {
        AlgorithmParameter mediumParameter = parameters.FirstOrDefault(ap => ap.Name == "MEDIUM");

        MediumSMA[instrument] = data[instrument].SMA(mediumParameter.Value, Ohlc.Close).ToList();
      }

      if (!SlowSMA.ContainsKey(instrument))
      {
        AlgorithmParameter slowParameter = parameters.FirstOrDefault(ap => ap.Name == "SLOW");

        SlowSMA[instrument] = data[instrument].SMA(slowParameter.Value, Ohlc.Close).ToList();
      }
    }
  }
}
