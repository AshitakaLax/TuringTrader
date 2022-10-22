using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.DataStructures
{
  /// <summary>
  /// This utilizes the Fidelity Export from thair Trader Pro.
  /// </summary>
  public class FidelityCsvBar
  {

    //[Name("Date")]
    public DateTime Date { get; set; }
    //[Name("Time")]
    public DateTime Time { get; set; }
    public double Open { get; set; }
    public double High { get; set; }
    //[Name("Low")]
    public double Low { get; set; }
    //[Name("Close")]
    public double Close { get; set; }
    //[Name("Volume")]
    public double Volume { get; set; }
  }
}
