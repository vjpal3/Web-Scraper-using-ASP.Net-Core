using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebScraperASP.Services
{
    public class ScraperNavigation : IScraperNavigation
    {
        private readonly IConfiguration config;

        public ScraperNavigation(IConfiguration config)
        {
            this.config = config;

        }
        public void LaunchBrowser(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://login.yahoo.com/");
        }

        public void CloseBrowser(IWebDriver driver)
        {
            Thread.Sleep(10000);
            driver.Quit();
        }

        public void Login(IWebDriver driver)
        {
            string username = "login-username";
            EnterUserCredentials(driver, username);

            string userPasswd = "login-passwd";
            EnterUserCredentials(driver, userPasswd);
        }

        public void EnterUserCredentials(IWebDriver driver, string userValue)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(userValue)));
            string userCredential = config.GetConnectionString(userValue);
            element.SendKeys(userCredential);
            element.SendKeys(Keys.Return);
        }

        public void GoToFinancePage(IWebDriver driver)
        {
            WebDriverWait waitForFinanceLink = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);

            try
            {
                driver.Navigate().GoToUrl("https://finance.yahoo.com");
            }
            catch (WebDriverException)
            {
                driver.Url = "https://finance.yahoo.com";
            }
        }

        public void GetListOfPortfolios(IWebDriver driver)
        {
            WebDriverWait waitForFolioList = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement folioList = waitForFolioList.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/portfolios']")));
            folioList.Click();
        }
    }
}
