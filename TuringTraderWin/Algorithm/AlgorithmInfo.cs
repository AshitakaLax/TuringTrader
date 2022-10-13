using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.Algorithm
{
  public class AlgorithmInfo : IAlgorithmInfo
  {
    /// <inheritdoc/>
    public string Description { get; set; }

    /// <inheritdoc/>
    public string Name { get; set; }

    /// <inheritdoc/>
    public bool IsChildAlgorithm => false;
  }
}
