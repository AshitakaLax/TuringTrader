using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;

namespace TuringTraderWin.Instruments
{
  public interface IInstrumentOld
  {
    /// <summary>
    /// Gets or sets the a nick name unique to the Instrument at run-time. 
    /// This should be unique from all other instruments.
    /// </summary>
    string NickName { get; set; }

    /// <summary>
    /// Gets or sets the Description of the instrument.
    /// </summary>
    string Description { get; set; }

    /// <summary>
    /// Gets or sets the name of the instrument.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Gets or sets the Instrument's fully qualified symbol. For stocks, this is identical
    /// to the ticker. For options, this will include the expiry date,
    /// direction, and strike price.
    /// </summary>
    string Ticker { get; set; }

    /// <summary>
    /// Gets or sets the Ticker's backing stock symbol, This is only utilized if this is an option.
    /// TODO move this into it's own interface.
    /// </summary>
    string BackingSymbol { get; set; }

    /// <summary>
    /// Don't use in the meantime till enhancement on interfaces.
    /// </summary>
    bool IsOption { get; set; }

    /// <summary>
    /// Don't use in the meantime till enhancement on interfaces.
    /// </summary>
    bool IsOptionPut { get; set; }

    ITimeSeries<DateTime> Time { get; }
    ITimeSeries<double> Open { get; }
    ITimeSeries<double> High { get; }
    ITimeSeries<double> Low { get; }
    ITimeSeries<double> Close { get; }
    ITimeSeries<double> Volume { get; }
  }
}
