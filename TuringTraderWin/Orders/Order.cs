
using TuringTraderWin.DataStructures;
using TuringTraderWin.Instruments;

namespace TuringTraderWin.Orders
{
  /// <summary>
  /// Order ticket
  /// </summary>
  public class Order
  {
    /// <inheritdoc/>
    public IInstrument Instrument { get; set; }

    /// <inheritdoc/>
    public Bar BarOfExecution { get; set; }

    public bool IsBuy { get; set; } = true;

    /// <inheritdoc/>
    public OrderType Type { get; set; }

    /// <inheritdoc/>
    public int Quantity { get; set; }

    /// <inheritdoc/>
    public double Price { get; set; }

    /// <inheritdoc/>
    public string Comment { get; set; }

    /// <inheritdoc/>
    public DateTime QueueTime { get; set; }
    
    /// <inheritdoc/>
    public Func<IInstrument, bool> Condition { get; set; }
  }
}
