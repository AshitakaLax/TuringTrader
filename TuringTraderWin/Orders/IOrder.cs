using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;
using TuringTraderWin.Instruments;

namespace TuringTraderWin.Orders
{
  /// <summary>
  /// The interface for an order ticket.
  /// </summary>
  public interface IOrder
  {
    /// <summary>
    /// Gets or sets the instrument this order is for.
    /// </summary>
    IInstrument Instrument { get; set; }

    /// <summary>
    /// Gets or sets th
    /// </summary>
    Bar BarOfExecution { get; set; }

    /// <summary>
    /// Gets or sets the type of order.
    /// </summary>
    OrderType Type { get; set; }

    /// <summary>
    /// Gets or sets the Quantity of the Order.
    /// </summary>
    int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the Price of the Order.
    /// </summary>
    double Price { get; set; }

    /// <summary>
    /// Gets or sets the User defined comments.
    /// </summary>
    string Comment { get; set; }

    /// <summary>
    /// Gets or sets the time the order was queued.
    /// </summary>
    DateTime QueueTime { get; set; }

    /// <summary>
    /// Gets or sets the exec condition
    /// </summary>
    Func<IInstrument, bool> Condition { get; set; }
  }
}
