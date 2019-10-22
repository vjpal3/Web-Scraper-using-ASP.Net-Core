using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScraperASP.Models
{
    public interface IScrapeInfoRepository
    {
        void AddScrapeInfo(List<string> scrapedInfo, string userId);
    }
}
