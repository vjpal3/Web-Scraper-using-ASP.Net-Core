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
    }
}
