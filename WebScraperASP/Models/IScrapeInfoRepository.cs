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
        int GetScrapeId(IDbConnection connection, string userId);
    }
}
