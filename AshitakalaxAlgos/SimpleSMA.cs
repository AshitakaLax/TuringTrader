
using System.Globalization;
using TuringTrader.Indicators;
using TuringTrader.Simulator;
namespace AshitakalaxAlgos
{
  public class SimpleSMA : Algorithm
  {
    #region internal data
    private Plotter _plotter = new Plotter();
    private readonly string _template = "SimpleChart";
    private readonly double _initialCash = 100000.00;
    private double? _initialPrice = null;
    private readonly string _instrumentNick = "SQQQ";
    [OptimizerParam(10, 30, 5, true)]
    public int EMA_FAST = 20;
    [OptimizerParam(35, 100, 5, true)]
    public int EMA_SLOW = 60;
    #endregion



    #region optimizable parameters
    //    [OptimizerParam(0, 100, 5)]
    public int STOCK_PCNT = 60;

    public override string Description => "A simple Moving Average";

    public override string Author => "Levi Balling";
    #endregion
    public override void Run()
    {
      StartTime = DateTime.Parse("10/01/2021", CultureInfo.InvariantCulture);
      EndTime = DateTime.Parse("10/01/2022", CultureInfo.InvariantCulture);
      Deposit(30000);
      // add instruments
      AddDataSource(_instrumentNick);
      
      foreach (var s in SimTimes)
      {
        Instrument instrument = Instruments.First();

        ITimeSeries<double> slow = instrument.Close.EMA(EMA_SLOW);
        ITimeSeries<double> fast = instrument.Close.EMA(EMA_FAST);

        int currentPosition = instrument.Position;

        // buy trigger
        int targetPosition = fast[0] > slow[0]
          ? (int)Math.Floor(NetAssetValue[0]
                            / instrument.Close[0]) // bullish => long
          : 0;                                     // bearish => flat


        // place orders
        instrument.Trade(targetPosition - currentPosition,
                         OrderType.openNextBar);

        if (_initialPrice == null) _initialPrice = instrument.Close[0];

        _plotter.SelectChart("indicators vs time", "date");
        _plotter.SetX(SimTime[0]);

        _plotter.Plot(instrument.Symbol, instrument.Close[0] / (double)_initialPrice);
        _plotter.Plot("MA Crossover", NetAssetValue[0] / _initialCash);
        _plotter.Plot("SMA(slow)", slow[0]);
        _plotter.Plot("SMA(fast)", fast[0]);
      }
    }
    public override void Report()
    {
      _plotter.OpenWith("SimpleReport");
    }

    public override string ToString()
    {
      return Name;
    }
  }
}
