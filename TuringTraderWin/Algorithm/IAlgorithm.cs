using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.Algorithm
{
  public interface IAlgorithm
  {
    /// <summary>
    /// Gets the information about the Algorithm.
    /// </summary>
    IAlgorithmInfo Info { get; }
  }
}
