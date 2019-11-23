using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebScraperASP.Models;

namespace WebScraperASP.ViewModels
{
    public interface ICombinedStockDataVMRepo
    {
        List<CombinedStockDataVM> GetRecentStocksData(IScrapeInfoRepository scrapeInfoRepo, string userId);

        List<CombinedStockDataVM> GetStocksDataByScrapeId(int scrapeId);
    }
}
