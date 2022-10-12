using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TuringTrader.Simulator;

namespace TuringTraderWin.Optimizer
{
  public class OptimizerManager : IOptimizerManager
  {
    /// <summary>
    /// Retrieve all optimizable parameters for algorithm.
    /// </summary>
    /// <param name="algo">input algorithm</param>
    /// <returns>optimizable parameters</returns>
    public IEnumerable<OptimizerParam> GetParams(IAlgorithm algo)
    {
      Type algoType = algo.GetType();

      IEnumerable<PropertyInfo> properties = algoType.GetProperties()
          .Where(p => Attribute.IsDefined(p, typeof(OptimizerParamAttribute)));

      foreach (PropertyInfo property in properties)
        yield return new OptimizerParam(algo, property.Name);

      IEnumerable<FieldInfo> fields = algoType.GetFields()
          .Where(p => Attribute.IsDefined(p, typeof(OptimizerParamAttribute)));

      foreach (FieldInfo field in fields)
        yield return new OptimizerParam(algo, field.Name);

      yield break;
    }

    /// <inheritdoc/>
    public int GetNumberOfIterations(IAlgorithm algo)
    {
      // figure out total number of iterations
      int numIterationsTotal = 1;
      foreach (OptimizerParam parameter in algo.OptimizerParams.Values)
      {
        int iterationsThisLevel = 0;
        if (parameter.IsEnabled)
        {
          for (int i = parameter.Start; i <= parameter.End; i += parameter.Step)
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
  }
}
