using System;
using System.Collections.Generic;
using System.Linq;
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
  }
}
