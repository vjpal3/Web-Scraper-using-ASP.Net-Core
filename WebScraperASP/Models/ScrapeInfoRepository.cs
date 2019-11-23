using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace WebScraperASP.Models
{
    public class ScrapeInfoRepository : IScrapeInfoRepository
    {
        private readonly IConfiguration config;

        public ScrapeInfoRepository(IConfiguration config)
        {
            this.config = config;
        }
        public void AddScrapeInfo(List<string> scrapedData, string userId)
        {
            using (IDbConnection connection = new SqlConnection(config.GetConnectionString("ScraperData")))
            {
                var today = DateTime.Today;
                var timeZone = "";
                try
                {
                    var data = scrapedData[0].Split('\t');
                    timeZone = data[5].Split(' ')[1].Trim();
                    var timeString = data[5].Split(' ')[0].Trim();

                    TimeSpan tspan = DateTime.ParseExact(timeString, "h:mmtt", CultureInfo.InvariantCulture).TimeOfDay;

                    today = today.Add(tspan);
                    Console.WriteLine(timeZone);
                    Console.WriteLine(today);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Time data not valid");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Time data");
                }

                connection.Execute("dbo.uspScrapesInfo_AddScrapeInfo @ScrapeDate, @TimeZone, @UserId", new ScrapeInfo { ScrapeDate = today, TimeZone = timeZone, UserId = userId });
            }
        }

        public ScrapeInfo GetScrapeInfo(IDbConnection connection, string userId)
        {
            List<ScrapeInfo> scrapes = connection.Query<ScrapeInfo>("dbo.uspScrapesInfo_GetLatest @UserId", new { UserId = userId }).ToList();
            return scrapes[0];
        }

        public ScrapeInfo GetScrapeInfo(IDbConnection connection, int scrapeId)
        {
            List<ScrapeInfo> scrapes = connection.Query<ScrapeInfo>("dbo.uspScrapesInfo_GetByScrapeId @ScrapeId", new { ScrapeId = scrapeId }).ToList();
            return scrapes[0];
        }

        public IEnumerable<ScrapeInfo> GetAllScrapesInfo(string userId)
        {
            using (IDbConnection connection = new SqlConnection(config.GetConnectionString("ScraperData")))
            {
                List<ScrapeInfo> scrapes = connection.Query<ScrapeInfo>("dbo.uspScrapesInfo_GetAllByUserId @UserId", new { UserId = userId }).ToList();
                return scrapes;
            }
        }
    }
}
