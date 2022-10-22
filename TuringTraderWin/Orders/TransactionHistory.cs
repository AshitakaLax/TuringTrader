using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.Orders
{
  /// <summary>
  /// This tracks the transactions.
  /// This isn't initially necessary, but it seems like it will be helpful for future features.
  /// </summary>
  public class TransactionHistory : ITransactionHistory
  {
    /// <summary>
    /// Gets or sets the List of Transactions.
    /// </summary>
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    public void AddTransaction(Transaction transaction)
    {
      Transactions.Add(transaction);
    }

    public void ClearTransactions()
    {
      Transactions.Clear();
    }

    public IEnumerable<Transaction> GetAllTransactions()
    {
      return Transactions;
    }

    public string GetTransactionString()
    {
      int numOfTrades = Transactions.Count;
      string transactionStr = $"Trades:{numOfTrades}";
      foreach(Transaction transaction in Transactions)
      {
        transactionStr += transaction.ToString() + Environment.NewLine;
      }
      return transactionStr;
    }
  }
}
