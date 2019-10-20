using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebScraperASP.Services
{
    public class ScraperNavigation : IScraperNavigation
    {
        public void LaunchBrowser(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://login.yahoo.com/");
        }

        public void CloseBrowser(IWebDriver driver)
        {
            Thread.Sleep(10000);
            driver.Quit();
        }
    }
}
