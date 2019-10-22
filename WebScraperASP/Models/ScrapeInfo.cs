using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScraperASP.Models
{
    public class ScrapeInfo
    {
        public int ScrapeId { get; set; }
        public DateTime ScrapeDate { get; set; }
        public string TimeZone { get; set; }
        public string UserId { get; set; }
    }
}
