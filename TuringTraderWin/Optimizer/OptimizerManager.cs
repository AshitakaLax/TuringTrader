using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
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

    public Dictionary<string, IEnumerable<AlgorithmParameter>> GenerateAllAlgorithmCombinations(IAlgorithm algo, List<AlgorithmParameter> selectedParameters)
    {
      Dictionary<string, IEnumerable<AlgorithmParameter>> parameterCombinations = new Dictionary<string, IEnumerable<AlgorithmParameter>>();
      List<List<int>> allAvailableParameter = new List<List<int>>();

      foreach (AlgorithmParameter param in selectedParameters)
      {
        List<int> paramOptions;
        if (param.IsEnabled)
        {
          paramOptions = param.getAllValues();
        }
        else
        {
          paramOptions = new List<int>() { param.Value };
        }
        allAvailableParameter.Add(paramOptions);
      }
      IEnumerable<IEnumerable<int>> allParameterCombinations = CartesianProduct(allAvailableParameter);

      // this is an array of simulation parameter arrays
      foreach (IEnumerable<int> simCombination in allParameterCombinations)
      {
        List<AlgorithmParameter> simParameters = new List<AlgorithmParameter>();
        string simName = $"{algo.Info.Name}_";
        for (int i = 0; i < selectedParameters.Count(); i++)
        {
          int value = simCombination.ToArray()[i];
          simParameters.Add(new AlgorithmParameter()
          {
            Name = selectedParameters[i].Name,
            Value = value,
            IsEnabled = selectedParameters[i].IsEnabled,
            Description = selectedParameters[i].Description,
          });
          simName += $"{value}-";
        }
        simName = simName.TrimEnd('-');
        parameterCombinations[simName] = simParameters;
      }
      return parameterCombinations;
    }
    private IEnumerable<IEnumerable<T>> CartesianProduct<T>(IEnumerable<IEnumerable<T>> sequences)
    {
      IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>() };
      return sequences.Aggregate(
        emptyProduct,
        (accumulator, sequence) =>
          from accseq in accumulator
          from item in sequence
          select accseq.Concat(new[] { item }));
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

    public IEnumerable<AlgorithmParameter> GetParams(IAlgorithm algo)
    {
      return AlgorithmParameterDictionary[algo];
    }

    public void SetAlgorithmParameters(IAlgorithm algo, IEnumerable<AlgorithmParameter> parameters)
    {
      AlgorithmParameterDictionary[algo] = parameters.ToList();
    }
  }
}
