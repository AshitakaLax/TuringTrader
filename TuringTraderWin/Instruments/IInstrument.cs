using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;

namespace TuringTraderWin.Instruments
{
  public interface IInstrument
  {
    /// <summary>
    /// Gets or sets the a nick name unique to the Instrument at run-time. 
    /// This should be unique from all other instruments.
    /// </summary>
    string NickName { get; set; }
    ITimeSeries<DateTime> Time { get; }
    ITimeSeries<double> Open { get; }
    ITimeSeries<double> High { get; }
    ITimeSeries<double> Low { get; }
    ITimeSeries<double> Close { get; }
    ITimeSeries<double> Volume { get; }
  }
}
