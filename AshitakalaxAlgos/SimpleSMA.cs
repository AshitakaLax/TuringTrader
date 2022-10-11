
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

        ITimeSeries<double> slow = instrument.Close.EMA(63);
        ITimeSeries<double> fast = instrument.Close.EMA(21);

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





    #region OptimizeSettings - walk-forward-optimization
    //private void OptimizeSettings()
    //{
    //  // we only optimize settings on the top instance,
    //  // not those used for walk-forward optimization
    //  if (!IsOptimizing)
    //  {
    //    // enable optimizer parameters
    //    foreach (var s in OptimizerParams)
    //      s.Value.IsEnabled = true;

    //    // run optimization
    //    var optimizer = new OptimizerGrid(this, false);
    //    var end = SimTime[0];
    //    var start = end - TimeSpan.FromDays(90);
    //    optimizer.Run(start, end);

    //    // apply parameters from best result
    //    var best = optimizer.Results
    //        .OrderByDescending(r => r.Fitness)
    //        .FirstOrDefault();
    //    optimizer.SetParametersFromResult(best);
    //  }
    //}
    #endregion
    #region Run - algorithm core
    //public override IEnumerable<Bar> Run(DateTime? startTime, DateTime? endTime)
    //{
    //  StartTime = startTime != null ? (DateTime)startTime : DateTime.Parse("01/01/2007", CultureInfo.InvariantCulture);
    //  EndTime = endTime != null ? (DateTime)endTime : DateTime.Now - TimeSpan.FromDays(5);
    //  WarmupStartTime = StartTime - TimeSpan.FromDays(90);

    //  CommissionPerShare = 0.015;
    //  Deposit(1e6);

    //  var stocks = AddDataSource("SPY");
    //  var bonds = AddDataSource("TLT");

    //  bool firstOptimization = true;
    //  foreach (var s in SimTimes)
    //  {
    //    // re-tune parameters on a monthly schedule
    //    if (firstOptimization || NextSimTime.Month != SimTime[0].Month)
    //      OptimizeSettings();
    //    firstOptimization = false;

    //    // rebalance on a monthly schedule
    //    if (NextSimTime.Month != SimTime[0].Month)
    //    {
    //      var stockPcnt = STOCK_PCNT / 100.0;
    //      var stockShares = (int)Math.Floor(NetAssetValue[0] * stockPcnt / stocks.Instrument.Close[0]);
    //      stocks.Instrument.Trade(stockShares - stocks.Instrument.Position);

    //      var bondPcnt = 1.0 - stockPcnt;
    //      var bondShares = (int)Math.Floor(NetAssetValue[0] * bondPcnt / bonds.Instrument.Close[0]);
    //      bonds.Instrument.Trade(bondShares - bonds.Instrument.Position);
    //    }

    //    // strategy output
    //    if (!IsOptimizing && TradingDays > 0)
    //    {
    //      _plotter.SelectChart("Net Asset Value", "Date");
    //      _plotter.SetX(SimTime[0]);
    //      _plotter.Plot("Stock/Bond Strategy", NetAssetValue[0]);
    //      _plotter.Plot("S&P 500", stocks.Instrument.Close[0]);

    //      _plotter.SelectChart("Stock Percentage", "Date");
    //      _plotter.SetX(SimTime[0]);
    //      _plotter.Plot("Stock Percentage", 100.0 * stocks.Instrument.Position * stocks.Instrument.Close[0] / NetAssetValue[0]);
    //    }
    //  }

    //  // fitness value used for walk-forward-optimization
    //  FitnessValue = NetAssetValue[0] / NetAssetValueMaxDrawdown;

    //  yield break;
    //}
    #endregion
    #region Report - output chart
    public override void Report()
    {
      _plotter.OpenWith("SimpleReport");
    }
    #endregion
  }
}
