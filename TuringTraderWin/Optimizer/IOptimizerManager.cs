using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.Algorithm;

namespace TuringTraderWin.Optimizer
{
  public interface IOptimizerManager
  {
    /// <summary>
    /// Retrieve all optimizable parameters for algorithm.
    /// </summary>
    /// <param name="algo">input algorithm</param>
    /// <returns>optimizable parameters</returns>
    IEnumerable<AlgorithmParameter> GetParams(IAlgorithm algo);
    /// <summary>
    /// Gets the number of iterations based on the optimizations provided.
    /// </summary>
    /// <param name="algo">The algorithm to utilize.</param>
    /// <returns>The number of iterations.</returns>
    int GetNumberOfIterations(IAlgorithm algo);
  }
}
