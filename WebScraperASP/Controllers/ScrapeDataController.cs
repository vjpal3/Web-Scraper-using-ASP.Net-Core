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
using WebScraperASP.ViewModels;

namespace WebScraperASP.Controllers
{
    [Authorize]
    public class ScrapeDataController : Controller
    {
        //private List<string> extractedData = new List<string>();
        public IWebDriver Driver { get; private set; }
        private readonly IScraperNavigation navigation;
        private readonly IDataExtraction dataExtraction;
        private readonly ICompanyRepository companyRepository;
        private readonly IScrapeInfoRepository scrapeInfoRepository;
        private readonly IStockDataRepository stockDataRepository;
        private readonly ICombinedStockDataVMRepo combinedStockDataVMRepo;
        public List<CombinedStockDataVM> CombinedStocksData { get; set; }
        public string UserId { get; set; }

        public ScrapeDataController(IScraperNavigation navigation, IDataExtraction dataExtraction, ICompanyRepository companyRepo, IScrapeInfoRepository scrapeInfoRepo, IStockDataRepository stockDataRepo, ICombinedStockDataVMRepo combinedStockDataVMRepo)
        {
            this.navigation = navigation;
            this.dataExtraction = dataExtraction;
            this.companyRepository = companyRepo;
            this.scrapeInfoRepository = scrapeInfoRepo;
            this.stockDataRepository = stockDataRepo;
            this.combinedStockDataVMRepo = combinedStockDataVMRepo;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StartScraper()
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");

            using (Driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options))
            {
                StartNavigation();
                StartDataExtraction();
                StopScraper();
            }

            SaveDataToDatabase();

            CombinedStocksData = GetRecentData();

            return View("~/Views/PastScrapes/Details.cshtml", CombinedStocksData);
        }

        private List<CombinedStockDataVM> GetRecentData()
        {
            return combinedStockDataVMRepo.GetRecentStocksData(scrapeInfoRepository, UserId);
        }

        private void StartNavigation()
        {
            navigation.BeginNavigation(Driver);
        }

        private void StartDataExtraction()
        {
            dataExtraction.ScrapeStockData(Driver);
            
        }

        private void SaveDataToDatabase()
        {
            companyRepository.AddCompany(dataExtraction.GetStockData());

            scrapeInfoRepository.AddScrapeInfo(dataExtraction.GetStockData(), UserId);

            stockDataRepository.AddStockData(dataExtraction.GetStockData(), scrapeInfoRepository, companyRepository, UserId);
        }

        private void StopScraper()
        {
            Thread.Sleep(10000);
            Driver.Quit();
        }
    }
}