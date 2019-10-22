using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScraperASP.Models
{
    public interface IStockDataRepository
    {
        void AddStockData(List<string> scrapedData, IScrapeInfoRepository scrapeInfoReo, ICompanyRepository companyRepo, string userId);

    }
}
