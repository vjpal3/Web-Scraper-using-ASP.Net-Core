using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScraperASP.Data.DapperServices
{
    public interface IDatabaseWriter
    {
        List<string> ScrapedData { get; set; }
        void WriteData();
    }
}
