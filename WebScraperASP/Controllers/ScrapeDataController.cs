using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebScraperASP.Services;

namespace WebScraperASP.Controllers
{
    [Authorize]
    public class ScrapeDataController : Controller
    {
        public IWebDriver Driver { get; private set; }
        private readonly IScraperNavigation navigation;
        private readonly IDataExtraction dataExtraction;
        private List<string> extractedData = new List<string>();

        public ScrapeDataController(IScraperNavigation navigation, IDataExtraction dataExtraction)
        {
            this.navigation = navigation;
            this.dataExtraction = dataExtraction;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StartScraper()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");

            using (Driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options))
            {
                StartNavigation();
                StartDataExtraction();
                StopScraper();
            }
            ViewBag.extractedString = string.Join(",", extractedData.ToArray());
            return View();
        }

        
        private void StartDataExtraction()
        {
            dataExtraction.ScrapeStockData(Driver);
            extractedData = dataExtraction.GetStockData();

        }

        private void StartNavigation()
        {
            navigation.LaunchBrowser(Driver);
            navigation.Login(Driver);
            navigation.GoToFinancePage(Driver);
            navigation.GetListOfPortfolios(Driver);
            navigation.OpenAPortfolio(Driver);
        }


        private void StopScraper()
        {
            navigation.CloseBrowser(Driver);
        }
    }
}