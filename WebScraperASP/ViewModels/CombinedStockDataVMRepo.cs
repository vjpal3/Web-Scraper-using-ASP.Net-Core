using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
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
        public List<CombinedStockDataVM> GetRecentStocksData(IScrapeInfoRepository scrapeInfoRepo,  string userId)
        {
            using (IDbConnection connection = new SqlConnection(config.GetConnectionString("ScraperData")))
            {
                ScrapeInfo scrapeInfo = scrapeInfoRepo.GetScrapeInfo(connection, userId);

                List<CombinedStockDataVM> stocksData = connection.Query<Company, StockData, CombinedStockDataVM>
                    ("dbo.uspStocksData_Companies_GetRecent @UserId",
                    MapResults,
                    new { UserId = userId }, splitOn: "LastPrice").ToList();

                stocksData[0].ScrapeInfo = scrapeInfo;
                return stocksData;
            }
        }

        public List<CombinedStockDataVM> GetStocksDataByScrapeId(IScrapeInfoRepository scrapeInfoRepo, int scrapeId)
        {
            using (IDbConnection connection = new SqlConnection(config.GetConnectionString("ScraperData")))
            {
                ScrapeInfo scrapeInfo = scrapeInfoRepo.GetScrapeInfo(connection, scrapeId);

                List<CombinedStockDataVM> stocksData = connection.Query<Company, StockData, CombinedStockDataVM>
                    ("dbo.uspStocksData_Companies_GetByScrapeId @ScrapeId",
                    MapResults,
                    new { ScrapeId = scrapeId }, splitOn: "LastPrice").ToList();

                stocksData[0].ScrapeInfo = scrapeInfo;
                return stocksData;
            }
        }

        private CombinedStockDataVM MapResults(Company company, StockData stockData)
        {
            var stocksData = new CombinedStockDataVM
            {
                StockData = stockData,
                Company = company
            };
            return stocksData;
        }
    }
}
