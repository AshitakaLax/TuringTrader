using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.Instruments
{
  public class InstrumentManager : IInstrumentManager
  {
    /// <inheritdoc/>
    public ConcurrentDictionary<IInstrument, int> Positions { get; set; } = new ConcurrentDictionary<IInstrument, int>();

    /// <inheritdoc/>
    public void AddInstrument(IInstrument instrument)
    {
      Positions[instrument] = 0;
    }

    /// <inheritdoc/>
    public IInstrument GetInstrument(string nickName)
    {
      return Positions.Keys.FirstOrDefault(instrument => instrument.NickName == nickName);
    }

    /// <inheritdoc/>
    public void RemoveInstrument(IInstrument instrument)
    {
      if(Positions.ContainsKey(instrument))
      {
        Positions.Remove(instrument, out _);
      }
    }
  }
}
