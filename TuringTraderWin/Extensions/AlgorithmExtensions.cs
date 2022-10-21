using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;
using TuringTraderWin.Instruments;

namespace TuringTraderWin.Extensions
{
  public static class AlgorithmExtensions
  {

    public static List<Bar> ByTicker(this ConcurrentDictionary<IInstrument, List<Bar>> data, string ticker)
    {
      return data.FirstOrDefault(pair => pair.Key.Ticker == ticker).Value;
    }
    public static IInstrument GetInstrumentByTicker(this ConcurrentDictionary<IInstrument, List<Bar>> data, string ticker)
    {
      return data.FirstOrDefault(pair => pair.Key.Ticker == ticker).Key;

    }
  }
}
