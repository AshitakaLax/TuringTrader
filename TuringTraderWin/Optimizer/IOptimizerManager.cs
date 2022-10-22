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
    IEnumerable<AlgorithmParameter> GetDefaultParams(IAlgorithm algo);

    /// <summary>
    /// Sets the Parameters for a specific Algorithm.
    /// </summary>
    /// <param name="algo">The algorithm to set the parameters for.</param>
    /// <param name="parameters">The parameters to set.</param>
    void SetAlgorithmParameters(IAlgorithm algo, IEnumerable<AlgorithmParameter> parameters);

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

    /// <summary>
    /// Gets all of the different Parameter combinations with names for each combination for the simulation.
    /// </summary>
    /// <param name="algo">The algo(simplify to just the name).</param>
    /// <param name="selectedParameters">the selected Parameters to utilize.</param>
    /// <returns></returns>
    Dictionary<string, IEnumerable<AlgorithmParameter>> GenerateAllAlgorithmCombinations(IAlgorithm algo, List<AlgorithmParameter> selectedParameters);
  }
}
