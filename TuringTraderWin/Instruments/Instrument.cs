using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;

namespace TuringTraderWin.Instruments
{
  public class Instrument : IInstrument
  {
    public string NickName { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public string Ticker { get; set; }
    public string BackingSymbol { get; set; }
    public bool IsOption { get; set; } = false;
    public bool IsOptionPut { get; set; } = false;
  }
}
