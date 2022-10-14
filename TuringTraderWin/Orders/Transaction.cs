using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;

namespace TuringTraderWin.Orders
{
  /// <summary>
  /// The tracking of an order.
  /// This is equivalent to the LogEntry
  /// </summary>
  public class Transaction
  {
    /// <summary>
    /// Gets or sets the Symbol associated with the Transaction.
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Type of Transaction.
    /// </summary>
    public TransactionType Type { get; set; }

    /// <summary>
    /// Gets or sets the Order associated with the Transaction.
    /// </summary>
    public IOrder Order { get; set; }

    /// <summary>
    /// Gets or sets the bar that triggered the order.
    /// </summary>
    public Bar BarOfExecution { get; set; }

    /// <summary>
    /// Gets or sets the Price for which the order was filled.
    /// </summary>
    public double FillPrice { get; set; }

    /// <summary>
    /// Gets or sets the Commission paid for the transaction.
    /// </summary>
    public double Commission { get; set; }
  }
}
