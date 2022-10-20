using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.Algorithm;
using TuringTraderWin.DataSource;
using TuringTraderWin.DataStructures;
using TuringTraderWin.Instruments;
using TuringTraderWin.Simulator;

namespace TuringTraderWin.SampleAlgorithms
{
  public class SimpleMovingAverage : IAlgorithm
  {
    public IAlgorithmInfo Info => new AlgorithmInfo()
    {
      Name = "Simple Moving Average",
      Description = "Utilizes a Simple moving averages t"
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
        Description = "The Fast Leg of the Simple Moving Average algorthm"
      },
      new AlgorithmParameter()
      {
        Name = "SLOW",
        Start = 35,
        End = 100,
        Value = 60,
        IncrementStepAmount = 5,
        Description = "The Slow Leg of the Simple Moving Average algorthm"
      }
    };
    /// <summary>
    /// The Progress Handler Callback.
    /// </summary>
    private Action<int> ProgressHandlerCallback;

    public IEnumerable<IInstrument> Initialize(ConcurrentDictionary<string, AlgorithmParameter> algorithmParameters, IDataSourceManager dataSourceManager, ISimulatorCore simulatorCore)
    {
      //Setup the Start and End times
      simulatorCore.StartTime = DateTime.Parse("10/01/2021", CultureInfo.InvariantCulture);
      simulatorCore.EndTime = DateTime.Parse("10/01/2022", CultureInfo.InvariantCulture);

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
        Description = "First SQQQ"
      });
      instruments.Add(new Instrument()
      {
        Name = "Nasdaq",
        BackingSymbol = "^IXIC",
        IsOption = false,
        IsOptionPut = false,
        NickName = "^IXIC",
        Ticker = "^IXIC",
        Description = "Comparing against Nasdaq"
      });

      // These instruments data will be loaded into the datamanager later.
      return instruments;
    }


    public void HandleBarIncrement(ConcurrentDictionary<string, Bar> currentBars, ISimulatorCore simulatorCore, IInstrumentManager instrumentManager)
    {
      // calculate the Simple moving average value for this interation.

      // Here is where the logic is.
      Bar sqqqBar = currentBars["SQQQ"];


      // items we will need.
      IInstrument instrument = instrumentManager.GetInstrument("SQQQ");
      // InstrumentManager to get all of the different investments we are interested in.
      // It would be good to get these by their Ticker Symbol.
      // We should be able to leverage all of the existing Extension methods that are apart of the original Turing Trader.


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
  }
}
