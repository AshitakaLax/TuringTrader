using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.Algorithm;

namespace TuringTraderWin.Optimizer
{
  public class OptimizerManager : IOptimizerManager
  {
    private readonly ILogger Logger;
    public OptimizerManager(ILogger<OptimizerManager> logger)
    {
      Logger = logger;
    }
    public ConcurrentDictionary<IAlgorithm, List<AlgorithmParameter>> AlgorithmParameterDictionary { get; set; } = new ConcurrentDictionary<IAlgorithm, List<AlgorithmParameter>>();

    /// <summary>
    /// Retrieve all optimizable parameters for algorithm.
    /// </summary>
    /// <param name="algo">input algorithm</param>
    /// <returns>optimizable parameters</returns>
    public IEnumerable<AlgorithmParameter> GetDefaultParams(IAlgorithm algo)
    {
      return algo.GetDefaultParameters;
    }

    /// <inheritdoc/>
    public int GetNumberOfIterations(IAlgorithm algo)
    {
      // figure out total number of iterations
      int numIterationsTotal = 1;
      foreach (AlgorithmParameter parameter in algo.GetDefaultParameters)
      {
        int iterationsThisLevel = 0;
        if (parameter.IsEnabled)
        {
          for (int i = parameter.Start; i <= parameter.End; i += parameter.IncrementStepAmount)
            iterationsThisLevel++;
        }
        else
        {
          iterationsThisLevel = 1;
        }

        numIterationsTotal *= iterationsThisLevel;
      }

      return numIterationsTotal;
    }

    public void SetAlgorithmParameters(IAlgorithm algo, IEnumerable<AlgorithmParameter> parameters)
    {
      throw new NotImplementedException();
    }
  }
}
