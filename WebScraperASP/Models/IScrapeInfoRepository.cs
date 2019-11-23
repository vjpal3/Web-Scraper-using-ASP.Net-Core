using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebScraperASP.Models
{
    public interface IScrapeInfoRepository
    {
        void AddScrapeInfo(List<string> scrapedData, string userId);
        ScrapeInfo GetScrapeInfo(IDbConnection connection, string userId);
        ScrapeInfo GetScrapeInfo(IDbConnection connection, int scrapeId);
        IEnumerable<ScrapeInfo> GetAllScrapesInfo(string userId);
    }
}
