using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;

namespace TuringTraderWin.Instruments
{
  public class Instrument : TimeSeries<Bar>, IInstrument
  {
    public Instrument()
    {
      Time = new BarSeriesAccessor<DateTime>((int t) => this[t].Time);
      Open = new BarSeriesAccessor<double>((int t) => this[t].Open);
      High = new BarSeriesAccessor<double>((int t) => this[t].High);
      Low = new BarSeriesAccessor<double>((int t) => this[t].Low);
      Close = new BarSeriesAccessor<double>((int t) => this[t].Close);
      Volume = new BarSeriesAccessor<double>((int t) => this[t].Volume);
    }

    public ITimeSeries<DateTime> Time { get; private set; }

    public ITimeSeries<double> Open { get; private set; }
    public ITimeSeries<double> High { get; private set; }
    public ITimeSeries<double> Low { get; private set; }
    public ITimeSeries<double> Close { get; private set; }
    public ITimeSeries<double> Volume { get; private set; }
    public string NickName { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public string Ticker { get; set; }
    public string BackingSymbol { get; set; }
    public bool IsOption { get; set; } = false;
    public bool IsOptionPut { get; set; } = false;
  }
}
