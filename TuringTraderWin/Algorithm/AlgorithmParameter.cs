using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.Algorithm
{
  public class AlgorithmParameter
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public int Value { get; set; }
    public int Start { get; set; }
    public int End { get; set; }
    public int IncrementStepAmount { get; set; }
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Gets all of the different potential values.
    /// </summary>
    /// <returns>The list of different values.</returns>
    public List<int> getAllValues()
    {
      List<int> values = new List<int>();
      for (int i = Start; i <= End; i += IncrementStepAmount)
        values.Add(i);

      return values;
    }
  }
}
