using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScraperASP.ViewModels
{
    public interface ICombinedStockDataVMRepo
    {
        List<CombinedStockDataVM> GetRecentStocksData(); 
    }
}
