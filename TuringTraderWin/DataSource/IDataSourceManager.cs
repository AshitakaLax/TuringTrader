﻿using System;
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

    /// <summary>
    /// Adds a Data Source to the Data Source Manager.
    /// </summary>
    /// <param name="dataSource"></param>
    void AddDataSource(IDataSource dataSource);

    /// <summary>
    /// Adds a Data Source to the Data Source Manager.
    /// </summary>
    /// <param name="ticker">The stock Ticker to get data about.</param>
    void AddDataSource(string ticker);

  }
}
