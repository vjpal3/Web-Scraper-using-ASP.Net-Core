using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebScraperASP.Models;
using WebScraperASP.Services;

namespace WebScraperASP.Controllers
{
    [Authorize]
    public class ScrapeDataController : Controller
    {
        private List<string> extractedData = new List<string>();
        public IWebDriver Driver { get; private set; }
        private readonly IScraperNavigation navigation;
        private readonly IDataExtraction dataExtraction;
        private readonly ICompanyRepository companyRepository;
        private readonly IScrapeInfoRepository scrapeInfoRepository;
        private readonly IStockDataRepository stockDataRepository;

        public ScrapeDataController(IScraperNavigation navigation, IDataExtraction dataExtraction, ICompanyRepository companyRepo, IScrapeInfoRepository scrapeInfoRepo, IStockDataRepository stockDataRepo)
        {
            this.navigation = navigation;
            this.dataExtraction = dataExtraction;
            this.companyRepository = companyRepo;
            this.scrapeInfoRepository = scrapeInfoRepo;
            this.stockDataRepository = stockDataRepo;
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
            
            return View();
        }

        private void StartNavigation()
        {
            navigation.BeginNavigation(Driver);
        }

        private void StartDataExtraction()
        {
            dataExtraction.ScrapeStockData(Driver);
            extractedData = dataExtraction.GetStockData();
        }

        private void SaveDataToDatabase()
        {
            companyRepository.AddCompany(dataExtraction.GetStockData());

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            scrapeInfoRepository.AddScrapeInfo(dataExtraction.GetStockData(), userId);

            stockDataRepository.AddStockData(dataExtraction.GetStockData(), scrapeInfoRepository, companyRepository, userId);
        }

        private void StopScraper()
        {
            Thread.Sleep(10000);
            Driver.Quit();
        }
    }
}