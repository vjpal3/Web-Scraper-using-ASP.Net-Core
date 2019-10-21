using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebScraperASP.Services
{
    public interface IScraperNavigation
    {
        void LaunchBrowser(IWebDriver driver);
        void CloseBrowser(IWebDriver driver);
        void Login(IWebDriver driver);

    }
}
