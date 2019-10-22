using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScraperASP.Models
{
    public class StockData
    {
        public int Id { get; set; }
        public int ScrapeId { get; set; }
        public int SymbolId { get; set; }
        public decimal? LastPrice { get; set; }
        public decimal? Change { get; set; }
        public decimal? PercentChange { get; set; }
        public decimal? PrevClose { get; set; }
        public decimal? OpenPrice { get; set; }
        public int? Shares { get; set; }
        public decimal? CostBasics { get; set; }
        public DateTime? TradeDate { get; set; }
        public decimal? PercentAnnualGain { get; set; }
        public decimal? FiftyTwoWeekHigh { get; set; }
        public decimal? FiftyTwoWeekLow { get; set; }
        public decimal? Bid { get; set; }
        public int? BidSize { get; set; }
        public decimal? Ask { get; set; }

        public int? AskSize { get; set; }
        public decimal? MarketCap { get; set; }
    }
}
