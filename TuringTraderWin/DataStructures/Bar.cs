
namespace TuringTraderWin.DataStructures
{
  /// <summary>
  /// Container class holding all fields of a single bar, most notably
  /// time stamps and price information. Bar objects are read-only in nature,
  /// therefore all values need to be provided during object construction.
  /// </summary>
  public class Bar
  {

    /// <summary>
    /// Create and initialize a bar object.
    /// </summary>
    /// <param name="ticker">ticker, most often same as symbol</param>
    /// <param name="time">Initializer for Time field</param>
    /// <param name="open">Initializer for Open field</param>
    /// <param name="high">Initializer for High field</param>
    /// <param name="low">Initializer for Low field</param>
    /// <param name="close">Initializer for Close field</param>
    /// <param name="volume">Initializer for Volume field</param>
    /// <param name="hasOHLC">Initializer for HasOHLC field</param>
    /// <param name="bid">Initializer for Bid field</param>
    /// <param name="ask">Initializer for Ask field</param>
    /// <param name="bidVolume">Initializer for BidVolume field</param>
    /// <param name="askVolume">Initializer for AskVolume field</param>
    /// <param name="hasBidAsk">Initializer for HasBidAsk field</param>
    /// <param name="optionExpiry">Initializer for OptionExpiry field</param>
    /// <param name="optionStrike">Initializer for OptionStrike field</param>
    /// <param name="optionIsPut">Initializer for OptionIsPut field</param>
    public Bar(
        string ticker, DateTime time,
        double open, double high, double low, double close, long volume, bool hasOHLC,
        double bid, double ask, long bidVolume, long askVolume, bool hasBidAsk,
        DateTime optionExpiry, double optionStrike, bool optionIsPut)
    {
      Symbol = ticker; // default value, changed for options below
      Time = time;

      Open = open;
      High = high;
      Low = low;
      Close = close;
      Volume = volume;
      HasOHLC = hasOHLC;

      Bid = bid;
      Ask = ask;
      BidVolume = bidVolume;
      AskVolume = askVolume;
      HasBidAsk = hasBidAsk;

      OptionExpiry = optionExpiry;
      OptionStrike = optionStrike;
      OptionIsPut = optionIsPut;
      IsOption = optionStrike != default(double);
      if (IsOption)
      {
        Symbol = string.Format("{0}{1:yyMMdd}{2}{3:D8}",
                    Symbol,
                    OptionExpiry,
                    OptionIsPut ? "P" : "C",
                    (int)Math.Floor(1000.0 * OptionStrike));
      }
    }

    /// <summary>
    /// Create new OHLC bar.
    /// </summary>
    /// <param name="ticker"></param>
    /// <param name="t"></param>
    /// <param name="o"></param>
    /// <param name="h"></param>
    /// <param name="l"></param>
    /// <param name="c"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    public Bar(string ticker, DateTime t, double o, double h, double l, double c, long v) : this(
          ticker, t,
          o, h, l, c, v, true,
          default(double), default(double), default(long), default(long), false,
          default(DateTime), default(double), false)
    { 
    }

    /// <summary>
    /// Create NewValue
    /// Create new OHLC bar, with the same value copied
    /// to the open, high, low, and close fields. This method 
    /// is typically used by algorithms that act as data sources.
    /// </summary>
    /// <param name="ticker">ticker symbol</param>
    /// <param name="t">time stamp</param>
    /// <param name="v">value</param>
    /// <returns>OHLC bar</returns>
    public Bar(string ticker, DateTime t, double v): this(
          ticker, t,
          v, v, v, v, 0, true,
          default(double), default(double), default(long), default(long), false,
          default(DateTime), default(double), false)
    {

    }

    /// <summary>
    /// Fully qualified instrument symbol. Examples are AAPL, or
    /// XSP080119C00152000.
    /// </summary>
    public string Symbol { get; private set; }

    /// <summary>
    /// Time stamp, with date and time
    /// </summary>
    public DateTime Time { get; private set; }

    /// <summary>
    /// Open price.
    /// </summary>
    public double Open { get; private set; }

    /// <summary>
    /// High price.
    /// </summary>
    public double High { get; private set; }

    /// <summary>
    /// Low price.
    /// </summary>
    public double Low { get; private set; }

    /// <summary>
    /// Close price.
    /// </summary>
    public double Close { get; private set; }

    /// <summary>
    /// Trading volume.
    /// </summary>
    public long Volume { get; private set; }

    /// <summary>
    /// Flag indicating availability of Open/ High/ Low/ Close pricing.
    /// </summary>
    public bool HasOHLC { get; private set; }

    /// <summary>
    /// Bid price.
    /// </summary>
    public double Bid { get; private set; }

    /// <summary>
    /// Asking price.
    /// </summary>
    public double Ask { get; private set; }

    /// <summary>
    ///  Bid volume.
    /// </summary>
    public long BidVolume { get; private set; }

    /// <summary>
    ///  Ask volume.
    /// </summary>
    public long AskVolume { get; private set; }

    /// <summary>
    /// Flag indicating availability of Bid/ Ask pricing.
    /// </summary>
    public bool HasBidAsk { get; private set; }

    /// <summary>
    /// Only valid for options: Option expiry date.
    /// </summary>
    public DateTime OptionExpiry { get; private set; }

    /// <summary>
    /// Only valid for options: Option strike price. 
    /// </summary>
    public double OptionStrike { get; private set; }

    /// <summary>
    /// Only valid for options: true for puts, false for calls.
    /// </summary>
    public bool OptionIsPut { get; private set; }

    /// <summary>
    /// Flag indicating validity of option fields.
    /// </summary>
    public bool IsOption { get; private set; }
    public long Index { get; private set; }
  }
}
