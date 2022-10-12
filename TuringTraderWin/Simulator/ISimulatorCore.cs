using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.Orders;

namespace TuringTraderWin.Simulator
{
  /// <summary>
  /// It simulator core iterface.
  /// </summary>
  public interface ISimulatorCore
  {
    /// <summary>
    /// Queue's an order with the simulator.
    /// </summary>
    /// <param name="order">The order to execute.</param>
    void QueueOrder(IOrder order);
  }
}
