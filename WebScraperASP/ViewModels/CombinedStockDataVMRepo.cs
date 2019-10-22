using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebScraperASP.Models;

namespace WebScraperASP.ViewModels
{
    public class CombinedStockDataVMRepo : ICombinedStockDataVMRepo
    {
        private readonly IConfiguration config;

        public CombinedStockDataVMRepo(IConfiguration config)
        {
            this.config = config;
        }
        public List<CombinedStockDataVM> GetRecentStocksData()
        {
            throw new NotImplementedException();
        }
    }
}
