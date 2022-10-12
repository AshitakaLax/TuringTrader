using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;

namespace TuringTraderWin.Instruments
{
  public interface IOptionInstrument : IInstrument
  {
    ITimeSeries<double> Bid { get; }
    ITimeSeries<double> Ask { get; }
    ITimeSeries<double> BidVolume { get; }
    ITimeSeries<double> AskVolume { get; }
  }
}
