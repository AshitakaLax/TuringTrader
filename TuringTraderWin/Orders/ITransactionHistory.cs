using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.Orders
{
  /// <summary>
  /// Used to track the history of transactions.
  /// </summary>
  public interface ITransactionHistory
  {
    /// <summary>
    /// Clears all of the transactions from the history.
    /// </summary>
    void ClearTransactions();

    /// <summary>
    /// Adds a Transaction to the Transaction History.
    /// </summary>
    /// <param name="transaction"></param>
    void AddTransaction(Transaction transaction);

    /// <summary>
    /// Gets all of the Transaction in the history.
    /// </summary>
    /// <returns>The List of Transactions.</returns>
    IEnumerable<Transaction> GetAllTransactions();

    /// <summary>
    /// Gets a string of all the transactions.
    /// </summary>
    /// <returns>The string with all of the transactions.</returns>
    string GetTransactionString();
  }
}
