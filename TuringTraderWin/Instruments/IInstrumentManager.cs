using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.Instruments
{
  /// <summary>
  /// Handles all of the instruments for a simulation.
  /// </summary>
  public interface IInstrumentManager
  {
    ConcurrentDictionary<IInstrument, int>  Positions { get; set; }
    void AddInstrument(IInstrument instrument);
    void RemoveInstrument(IInstrument instrument);

    IInstrument GetInstrument(string nickName);

  }
}
