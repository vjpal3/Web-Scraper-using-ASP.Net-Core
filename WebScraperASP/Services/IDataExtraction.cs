using System.Collections.Generic;
using OpenQA.Selenium;

namespace WebScraperASP.Services
{
    public interface IDataExtraction
    {
        void ScrapeStockData(IWebDriver driver);
        List<string> GetStockData();
    }
}