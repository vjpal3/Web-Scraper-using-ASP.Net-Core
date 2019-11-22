using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebScraperASP.Models;

namespace WebScraperASP.Controllers
{
    [Authorize]
    public class PastScrapesController : Controller
    {

        private readonly IScrapeInfoRepository scrapeInfoRepository;

        public PastScrapesController(IScrapeInfoRepository scrapeInfoRepository)
        {
            this.scrapeInfoRepository = scrapeInfoRepository;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = scrapeInfoRepository.GetAllScrapesInfo(userId);
            return View(model);
        }

        public IActionResult Details(int id)
        {
            throw new NotImplementedException();
        }


    }
}