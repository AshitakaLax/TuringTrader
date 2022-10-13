using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTraderWin.DataStructures;

namespace TuringTraderWin.DataSource
{
  public class YahooDataSource : IDataSource
  {

    //TODO: Utilize existing libraries to pull in dependency on Yahoo Finance. Also check whether the data already exists in a Cache
    // We will need to have a cache that exists prior to that.
    public List<Bar> CachedData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IEnumerable<Bar> LoadData(DateTime startTime, DateTime endTime)
    {
      throw new NotImplementedException();
    }

    public void UpdateData(List<Bar> data, DateTime startTime, DateTime endTime)
    {
      throw new NotImplementedException();
    }
  }
}
