using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.DataStructures
{
  /// <summary>
  /// This utilizes the getSandP.py script to fetch the stocks for a specific time range
  /// </summary>
  public class CsvBar
  {
    public DateTime Date { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Open { get; set; }
    public double Close { get; set; }
    public double Volume { get; set; }

    [Name("Adj Close")]
    public double AdjClose { get; set; }

    public string Name { get; set; }
  }
}
