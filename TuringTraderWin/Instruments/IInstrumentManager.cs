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
    /// <summary>
    /// The number of Contracts or Shares for a specific Instrument.
    /// </summary>
    ConcurrentDictionary<IInstrument, int>  Positions { get; set; }
    
    /// <summary>
    /// Adds an new instrument.
    /// </summary>
    /// <param name="instrument">The instrument to add.</param>
    void AddInstrument(IInstrument instrument);

    /// <summary>
    /// removes an instrument.
    /// </summary>
    /// <param name="instrument">The instrument to remove.</param>

    void RemoveInstrument(IInstrument instrument);

    /// <summary>
    /// Gets a specific Instrument.
    /// </summary>
    /// <param name="nickName"></param>
    /// <returns></returns>
    IInstrument GetInstrument(string nickName);

  }
}
