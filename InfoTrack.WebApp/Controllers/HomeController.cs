using System;
using System.Diagnostics;
using System.Threading.Tasks;
using InfoTrack.Core.Contracts;
using InfoTrack.Core.Enums;
using InfoTrack.Core.Helpers;
using InfoTrack.Core.Models;
using InfoTrack.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InfoTrack.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScraperService _scraperService;
        private readonly ISearchService _searchService;
        private readonly IArticleService _articleService;

        public HomeController(ILogger<HomeController> logger, IScraperService scraperService, ISearchService searchService, IArticleService articleService)
        {
            _logger = logger;
            _scraperService = scraperService;
            _searchService = searchService;
            _articleService = articleService;
        }

        public async Task<ActionResult> Index()
        {
            const string query = "land registry searches";
            const string uri = "www.infotrack.co.uk";

            var search = new Search
            {
                SearchEngine = SearchEngine.Google,
                Query = query,
                Uri = new Uri(UriHelper.ToUri(uri))
            };

            search = await _searchService.Add(search);
            var articles = await _scraperService.FindArticleMatches(search);
            foreach (var article in articles) await _articleService.Add(article);
            return View();
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var error = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            _logger.LogError("Error on Home", error);
            return View(error);
        }
    }
}
