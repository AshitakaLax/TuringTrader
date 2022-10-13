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

    public void Initialize(ConcurrentDictionary<string, AlgorithmParameter> algorithmParameters, IDataSourceManager dataSourceManager, ISimulatorCore simulatorCore)
    {
      //Setup the Start and End times
      simulatorCore.StartTime = DateTime.Parse("10/01/2021", CultureInfo.InvariantCulture);
      simulatorCore.EndTime = DateTime.Parse("10/01/2022", CultureInfo.InvariantCulture);

      // Setup the initial Deposit
      simulatorCore.Deposit(30000);
      // Setup Data Sources
      dataSourceManager.
      // 
    }
    public void HandleBarIncrement(Bar previousBar)
    {
      throw new NotImplementedException();
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
