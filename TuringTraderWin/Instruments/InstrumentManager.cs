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
    public ConcurrentDictionary<IInstrument, int> Positions { get; set; }

    /// <inheritdoc/>
    public void AddInstrument(IInstrument instrument)
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IInstrument GetInstrument(string nickName)
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public void RemoveInstrument(IInstrument instrument)
    {
      throw new NotImplementedException();
    }
  }
}
