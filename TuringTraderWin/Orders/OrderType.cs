
namespace TuringTraderWin.Orders
{
  /// <summary>
 /// Enumeration of order types.
 /// </summary>
  public enum OrderType
  {
    //----- user transactions

    /// <summary>
    /// deposit/ withdraw cash
    /// </summary>
    cash,

    /// <summary>
    /// execute order at close of current bar
    /// </summary>
    closeThisBar,

    /// <summary>
    /// execute order at open of next bar
    /// </summary>
    openNextBar,

    /// <summary>
    /// execute stop order on next bar
    /// </summary>
    stopNextBar,

    /// <summary>
    /// execute limit order on next bar
    /// </summary>
    limitNextBar,

    //----- simulator-internal transactions

    /// <summary>
    /// expire option at close of current bar. this order type is
    /// reserved for internal use by the simulator engine.
    /// </summary>
    optionExpiryClose,

    /// <summary>
    /// close out a position in an inactive stock
    /// </summary>
    instrumentDelisted,

    /// <summary>
    /// fake close at end of simulation
    /// </summary>
    endOfSimFakeClose,
  }
}
