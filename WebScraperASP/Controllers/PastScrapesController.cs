using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebScraperASP.Models;
using WebScraperASP.ViewModels;

namespace WebScraperASP.Controllers
{
    [Authorize]
    public class PastScrapesController : Controller
    {

        private readonly IScrapeInfoRepository scrapeInfoRepository;
        private readonly ICombinedStockDataVMRepo combinedStockDataVMRepo;

        public PastScrapesController(IScrapeInfoRepository scrapeInfoRepository, ICombinedStockDataVMRepo combinedStockDataVMRepo)
        {
            this.scrapeInfoRepository = scrapeInfoRepository;
            this.combinedStockDataVMRepo = combinedStockDataVMRepo;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = scrapeInfoRepository.GetAllScrapesInfo(userId);
            return View(model);
        }

        public ViewResult Details(int id)
        {
            var model = combinedStockDataVMRepo.GetStocksDataByScrapeId(scrapeInfoRepository, id);
            return View(model);
        }
    }
}