using TuringTraderWin.DataStructures;

namespace TuringTraderWin.Indicators
{
  /// <summary>
  /// Set of Extensions that will allow for calculating Indicators.
  /// </summary>
  public static class IndicatorExtensions
  {
    /// <summary>
    /// Calculates the Simple Moving Average for the data provided.
    /// </summary>
    /// <param name="bars">The data to utilize to calculate the Simple moving average.</param>
    /// <param name="period">The number of rolling samples to use to calculate the average.</param>
    /// <param name="ohlc">The source of the data from the bar.</param>
    /// <returns>The Simple Moving Average.</returns>
    /// <exception cref="ArgumentOutOfRangeException">only valid input.</exception>
    public static IEnumerable<double> SMA(this List<Bar> bars, int period, Ohlc ohlc)
    {
      if(period < 0)
      {
        throw new ArgumentOutOfRangeException(nameof(period));
      }

      var queue = new Queue<double>(period);
      double sum = 0;
      foreach (Bar bar in bars)
      {
        if (queue.Count == period)
        {
          yield return sum / period;
          sum -= queue.Dequeue();
        }

        double newValue = 0.0;
        switch (ohlc)
        {
          case Ohlc.Open:
            newValue = bar.Open;
            break;
          case Ohlc.Close:
            newValue = bar.Close;
            break;
          case Ohlc.High:
            newValue = bar.High;
            break;
          case Ohlc.Low:
            newValue = bar.Low;
            break;
          default:
            break;
        }

        sum += newValue;
        queue.Enqueue(newValue);
      }
      yield return sum / period;
    }
  }
}
