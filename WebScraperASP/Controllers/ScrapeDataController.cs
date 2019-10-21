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

        public ScrapeDataController(IScraperNavigation navigation)
        {
            this.navigation = navigation;
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
                //StartDataCollection();
                StopScraper();
            }
            
            return View();
        }

        private void StartNavigation()
        {
            navigation.LaunchBrowser(Driver);
        }

        private void StopScraper()
        {
            navigation.CloseBrowser(Driver);
        }


        
    }
}