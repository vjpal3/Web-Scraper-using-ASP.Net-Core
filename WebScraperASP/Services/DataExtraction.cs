using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebScraperASP.Services
{
    public class DataExtraction : IDataExtraction
    {
        private readonly List<string> stockData = new List<string>();

        public List<string> GetStockData()
        {
            return stockData;
        }

        public void ScrapeStockData(IWebDriver driver)
        {
            AccessStockData(driver);
            ExtractStockData(driver);
        }
        private void AccessStockData(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table")));

            ExtractStockData(driver);
        }

        private void ExtractStockData(IWebDriver driver)
        {
            int rowCount = driver.FindElements(By.XPath("//table/tbody/tr")).Count;
            int columnCount = driver.FindElements(By.XPath("//table/tbody/tr[1]//td")).Count;
            for (int i = 1; i <= rowCount; i++)
            {
                string cellData = "";
                for (int j = 1; j <= columnCount - 2; j++)
                {
                    cellData += driver.FindElement(By.XPath("//table[@class='W(100%)']/tbody/tr[" + i + "]/td[" + j + "]")).Text + "\t";
                }
                stockData.Add(cellData);
            }
        }

        
    }
}
