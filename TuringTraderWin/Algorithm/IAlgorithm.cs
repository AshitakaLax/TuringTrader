using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataSource;
using TuringTraderWin.DataStructures;
using TuringTraderWin.Instruments;

namespace TuringTraderWin.Algorithm
{
  public interface IAlgorithm
  {
    /// <summary>
    /// Gets the information about the Algorithm.
    /// </summary>
    IAlgorithmInfo Info { get; }

    /// <summary>
    /// Initializes all of the items needed to run the algorithm.
    /// </summary>
    void Initialize(ConcurrentDictionary<string, AlgorithmParameter> algorithmParameters, IDataSourceManager dataSourceManager);

    /// <summary>
    /// This is called each bar increment. 
    /// </summary>
    /// <param name="previousBar"></param>
    void HandleBarIncrement(Bar previousBar);
    IEnumerable<AlgorithmParameter> GetDefaultParameters { get; }

    //TODO Run Async with a Result that has the status of the Run. This would work better if you are running multiple Simulations in parallel.

    /// <summary>
    /// Sets the handler of of the Progress during the running of the algorithm.
    /// </summary>
    /// <param name="progressHandler">The function to call that you pass the progress(int) from 0 to 100</param>
    void SetProgressHandler(Action<int> progressHandler);
  }
}
