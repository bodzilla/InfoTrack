using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using InfoTrack.Core.Contracts;
using InfoTrack.Core.Helpers;
using InfoTrack.Core.Models;

namespace InfoTrack.Core.Services
{
    public sealed class ScraperService : IScraperService
    {
        private readonly IScraper _scraper;

        public ScraperService(IScraper scraper) => _scraper = scraper;

        /// <inheritdoc />
        public async Task<IEnumerable<Article>> FindMatchingArticles(Search search)
        {
            var articles = new List<Article>();
            var baseUrl = UriHelper.ToBaseUrl(search.Uri);
            var scrapedArticles = await _scraper.GetArticles(search.Query);
            if (scrapedArticles == null) return articles;

            for (int i = 0; i < scrapedArticles.Count; i++)
            {
                var scrapedUrl = scrapedArticles[i].SelectSingleNode(@"./a").Attributes[0].Value;
                var scrapedTitle = HttpUtility.HtmlDecode(scrapedArticles[i].InnerText);

                if (!scrapedUrl.Contains(baseUrl, StringComparison.CurrentCultureIgnoreCase)) continue;

                var article = new Article
                {
                    Search = search,
                    Title = scrapedTitle,
                    Rank = i + 1, // Users will count starting from 1, not 0.
                    Uri = new Uri(scrapedUrl)
                };

                articles.Add(article);
            }
            return articles;
        }
    }
}
