using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScraperASP.Models
{
    public interface ICompanyRepository
    {
        void AddCompany(List<string> scrapedData);
    }
}
