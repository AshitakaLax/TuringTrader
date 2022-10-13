using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringTraderWin.DataSource
{
  /// <summary>
  /// Manages the creation of various Data Sources.
  /// </summary>
  public interface IDataSourceManager
  {
    /// <summary>
    /// Gets the Data Source.
    /// </summary>
    /// <returns></returns>
    IDataSource GetDataSource();
  }
}
