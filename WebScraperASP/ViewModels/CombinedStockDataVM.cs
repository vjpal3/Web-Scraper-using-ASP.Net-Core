using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebScraperASP.Models;

namespace WebScraperASP.ViewModels
{
    public class CombinedStockDataVM
    {
        public StockData StockData { get; set; }
        public Company Company { get; set; }
        public ScrapeInfo ScrapeInfo { get; set; }
    }
}
