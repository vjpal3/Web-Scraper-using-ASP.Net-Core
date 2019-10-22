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
    public class StockDataRepository : IStockDataRepository
    {
        private readonly IConfiguration config;

        public StockDataRepository(IConfiguration config)
        {
            this.config = config;
        }
        public void AddStockData(List<string> scrapedData, IScrapeInfoRepository scrapeInfoRepo, ICompanyRepository companyRepo, string userId)
        {
            using (IDbConnection connection = new SqlConnection(config.GetConnectionString("ScraperData")))
            {
                var stocksData = new List<StockData>();
                int scrapeId = scrapeInfoRepo.GetScrapeInfo(connection, userId).ScrapeId;
                
                foreach (var item in scrapedData)
                {
                    var data = item.Split('\t');

                    int symbolId = companyRepo.GetSymbolId(connection, data[0].Trim());
                    
                    decimal? lastPrice = ParseDecimalString(data[2].Trim());
                    decimal? change = ParseDecimalString(data[3].Trim());
                    decimal? percentChange = ParseDecimalString(data[4].Trim().TrimEnd('%'));
                    decimal? prevClose = ParseDecimalString(data[6].Trim());
                    decimal? openPrice = ParseDecimalString(data[7].Trim());
                    int? shares = ParseIntString(data[8].Trim());
                    decimal? costBasics = ParseDecimalString(data[9].Trim());

                    DateTime tradeDate1;
                    DateTime? tradeDate = DateTime.TryParse(data[10].Trim(), out tradeDate1) ? tradeDate1
                        : (DateTime?)null;

                    decimal? percentAnnualGain = ParseDecimalString(data[11].Trim().TrimEnd('%'));
                    decimal? fiftyTwoWeekHigh = ParseDecimalString(data[12].Trim());
                    decimal? fiftyTwoWeekLow = ParseDecimalString(data[13].Trim());
                    decimal? bid = ParseDecimalString(data[14].Trim());
                    int? bidSize = ParseIntString(data[15].Trim());
                    decimal? ask = ParseDecimalString(data[16].Trim());
                    int? askSize = ParseIntString(data[17].Trim());
                    decimal? marketCap = ParseMarketCap(data[18].Trim());

                    stocksData.Add(new StockData
                    {
                        ScrapeId = scrapeId,
                        SymbolId = symbolId,
                        LastPrice = lastPrice,
                        Change = change,
                        PercentChange = percentChange,
                        PrevClose = prevClose,
                        OpenPrice = openPrice,
                        Shares = shares,
                        CostBasics = costBasics,
                        TradeDate = tradeDate,
                        PercentAnnualGain = percentAnnualGain,
                        FiftyTwoWeekHigh = fiftyTwoWeekHigh,
                        FiftyTwoWeekLow = fiftyTwoWeekLow,
                        Bid = bid,
                        BidSize = bidSize,
                        Ask = ask,
                        AskSize = askSize,
                        MarketCap = marketCap
                    });
                }
                connection.Execute("dbo.uspStocksData_AddStockData @ScrapeId, @SymbolId, @LastPrice, @Change, @PercentChange, @PrevClose, @OpenPrice, @Shares, @CostBasics, @TradeDate, @PercentAnnualGain, @FiftyTwoWeekHigh, @FiftyTwoWeekLow, @Bid, @BidSize, @Ask, @AskSize, @MarketCap", stocksData);
            }
        }

        private decimal? ParseDecimalString(string strDecimal)
        {
            return decimal.TryParse(strDecimal, out decimal result) ? result : (decimal?)null;
        }

        private int? ParseIntString(string strInt)
        {
            return int.TryParse(strInt, out int result) ? result : (int?)null;
        }

        private decimal? ParseMarketCap(string marketCap)
        {
            char capUnit = marketCap[marketCap.Length - 1];
            decimal? marketCapValue = 0;
            switch (capUnit)
            {
                case 'B':
                    marketCapValue = ParseDecimalString(marketCap.TrimEnd('B')) * (decimal?)Math.Pow(10.00, 3.00);
                    break;

                case 'T':
                    marketCapValue = ParseDecimalString(marketCap.TrimEnd('T')) * (decimal?)Math.Pow(10.00, 6.00);
                    break;

                case 'M':
                    marketCapValue = ParseDecimalString(marketCap.TrimEnd('M'));
                    break;

                default:
                    marketCapValue = ParseDecimalString(marketCap);
                    break;
            }
            return marketCapValue;
        }
    }
}
