using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.Algorithm;
using TuringTraderWin.DataStructures;
using TuringTraderWin.Instruments;
using TuringTraderWin.Orders;
using TuringTraderWin.Simulator;

namespace TuringTraderWin.Extensions
{
  public static class SimulatorExtensions
  {
    public static Order Trade(this IAlgorithm algorithm, IInstrument instrument, ISimulatorCore simulatorCore, int quantity, Bar bar, OrderType tradeExecution = OrderType.openNextBar, double price = 0.00, Func<IInstrument, bool> condition = null)
    {
      if (quantity == 0)
        return null;


      Order order = new Order()
      {
        Instrument = instrument,
        BarOfExecution = bar,
        Quantity = quantity,
        Type = tradeExecution,
        Price = price,
        Condition = condition,
      };
      simulatorCore.QueueOrder(order);
      return order;
    }
  }
}
