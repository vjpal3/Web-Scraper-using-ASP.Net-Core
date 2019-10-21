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
using WebScraperASP.Data.DapperServices;
using WebScraperASP.Services;

namespace WebScraperASP.Controllers
{
    [Authorize]
    public class ScrapeDataController : Controller
    {
        public IWebDriver Driver { get; private set; }
        private readonly IScraperNavigation navigation;
        private readonly IDataExtraction dataExtraction;
        private readonly IDatabaseWriter databaseWriter;

        //private List<string> extractedData = new List<string>();

        public ScrapeDataController(IScraperNavigation navigation, IDataExtraction dataExtraction, IDatabaseWriter dbWriter)
        {
            this.navigation = navigation;
            this.dataExtraction = dataExtraction;
            this.databaseWriter = dbWriter;
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
                SaveDataToDatabase();
                StopScraper();
            }
            //ViewBag.extractedString = string.Join(",", extractedData.ToArray());
            return View();
        }

        private void StartNavigation()
        {
            navigation.LaunchBrowser(Driver);
            navigation.Login(Driver);
            navigation.GoToFinancePage(Driver);
            navigation.GetListOfPortfolios(Driver);
            navigation.OpenAPortfolio(Driver);
        }

        private void StartDataExtraction()
        {
            dataExtraction.ScrapeStockData(Driver);
        }

        private void SaveDataToDatabase()
        {
            databaseWriter.ScrapedData = dataExtraction.GetStockData();
            databaseWriter.WriteData();
        }

        private void StopScraper()
        {
            navigation.CloseBrowser(Driver);
        }
    }
}