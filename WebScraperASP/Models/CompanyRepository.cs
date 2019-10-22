using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace WebScraperASP.Models
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IConfiguration config;

        public CompanyRepository(IConfiguration config)
        {
            this.config = config;
        }
        public void AddCompany(List<string> scrapedData)
        {
            using (IDbConnection connection = new SqlConnection(config.GetConnectionString("ScraperData")))
            {
                var companies = new List<Company>();

                foreach (var item in scrapedData)
                {
                    var data = item.Split('\t');
                    companies.Add(new Company { SymbolName = data[0].Trim(), CompanyName = data[1].Trim() });
                }
                connection.Execute("dbo.uspCompanies_AddCompany @SymbolName, @CompanyName", companies);
            }
        }

        public int GetSymbolId(IDbConnection connection, string symbolName)
        {
            List<Company> companies = connection.Query<Company>("dbo.uspCompanies_GetSymbol @SymbolName", new { SymbolName = symbolName }).ToList();

            return companies[0].Id;
        }
    }
}
