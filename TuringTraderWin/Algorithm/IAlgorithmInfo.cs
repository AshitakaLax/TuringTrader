using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.Algorithm
{
  /// <summary>
  /// Provides the information needed setting up the correct data sources and such.
  /// </summary>
  public interface IAlgorithmInfo
  {
    string Description { get; set; }
    string Name { get; set; }
    /// <summary>
    /// Gets a value indicating whether the Algorithm can run as a child.
    /// </summary>
    bool IsChildAlgorithm { get; }
  }
}
