using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.Orders
{
  public enum TransactionType
  {
    /// <summary>
    /// cash transaction
    /// </summary>
    Cash,

    /// <summary>
    /// stock or index
    /// </summary>
    Equity,

    /// <summary>
    /// put option
    /// </summary>
    OptionPut,

    /// <summary>
    /// call option
    /// </summary>
    OptionCall,
  };
}
