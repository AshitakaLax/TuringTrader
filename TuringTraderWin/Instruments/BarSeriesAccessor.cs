using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;

namespace TuringTraderWin.Instruments
{
  public class BarSeriesAccessor<T> : ITimeSeries<T>
  {
    private Func<int, T> _accessor;

    public BarSeriesAccessor(Func<int, T> accessor)
    {
      _accessor = accessor;
    }

    public T this[int barsBack]
    {
      get
      {
        return _accessor(barsBack);
      }
    }
  }
}
