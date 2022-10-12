using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.Instruments;

namespace TuringTraderWin.Orders
{
  /// <summary>
  /// Order ticket
  /// </summary>
  public class Order : IOrder
  {
    /// <summary>
    /// instrument this order is for
    /// </summary>
    public IInstrument Instrument;

    /// <summary>
    /// type of order
    /// </summary>
    public OrderType Type;

    /// <summary>
    /// quantity of order
    /// </summary>
    public int Quantity;

    /// <summary>
    /// price of order, only required for stop orders
    /// </summary>
    public double Price;

    /// <summary>
    /// user-defined comment
    /// </summary>
    public string Comment;

    /// <summary>
    /// time stamp of queuing this order
    /// </summary>
    public DateTime QueueTime;

    /// <summary>
    /// exec condition
    /// </summary>
    public Func<IInstrument, bool> Condition = null;
  }
}
