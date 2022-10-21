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
    public static Order Buy(this IAlgorithm algorithm, IInstrument instrument, ISimulatorCore simulatorCore, Bar bar, int quantity = 0, OrderType tradeExecution = OrderType.openNextBar, double price = 0.00, Func<IInstrument, bool> condition = null)
    {
      Order order = new Order()
      {
        Instrument = instrument,
        BarOfExecution = bar,
        Quantity = quantity,
        Type = tradeExecution,
        Price = price,
        IsBuy = true,
        Condition = condition,
      };
      simulatorCore.QueueOrder(order);
      return order;
    }
    public static Order Sell(this IAlgorithm algorithm, IInstrument instrument, ISimulatorCore simulatorCore, Bar bar, int quantity = 0, OrderType tradeExecution = OrderType.openNextBar, double price = 0.00, Func<IInstrument, bool> condition = null)
    {
      Order order = new Order()
      {
        Instrument = instrument,
        BarOfExecution = bar,
        Quantity = quantity,
        Type = tradeExecution,
        IsBuy = false,
        Price = price,
        Condition = condition,
      };
      simulatorCore.QueueOrder(order);
      return order;
    }
  }
}
