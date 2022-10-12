using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;

namespace TuringTraderWin.Instruments
{
  public class OptionInstrument : Instrument, IOptionInstrument
  {
    public OptionInstrument() : base()
    {
      Bid = new BarSeriesAccessor<double>((int t) => this[t].Bid);
      Ask = new BarSeriesAccessor<double>((int t) => this[t].Ask);
      BidVolume = new BarSeriesAccessor<double>((int t) => this[t].BidVolume);
      AskVolume = new BarSeriesAccessor<double>((int t) => this[t].AskVolume);
    }


    public ITimeSeries<double> Bid { get; private set; }
    public ITimeSeries<double> Ask { get; private set; }
    public ITimeSeries<double> BidVolume { get; private set; }
    public ITimeSeries<double> AskVolume { get; private set; }
  }
}
