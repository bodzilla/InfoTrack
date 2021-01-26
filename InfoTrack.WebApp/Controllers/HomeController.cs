using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using InfoTrack.Core.Contracts;
using InfoTrack.Core.Models;
using InfoTrack.WebApp.Models;
using InfoTrack.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InfoTrack.WebApp.Controllers
{
    public sealed class HomeController : Controller
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

        public IActionResult Index() => View();

        public async Task<ActionResult> Search(Search search)
        {
            var articles = (await _scraperService.FindMatchingArticles(search)).ToList();
            search = await _searchService.Add(search);
            foreach (var article in articles) await _articleService.Add(article);
            var resultViewModel = new ResultViewModel { Search = search, Articles = articles };
            return View(resultViewModel);
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
