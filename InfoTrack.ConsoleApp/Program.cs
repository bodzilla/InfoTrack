using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using InfoTrack.Core.Enums;
using InfoTrack.Core.Helpers;
using InfoTrack.Core.Models;
using InfoTrack.Persistence;
using InfoTrack.Scraper;

namespace InfoTrack.ConsoleApp
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var uri = new Uri(UriHelper.ToUri(args[1]));
            new DapperConfiguration().ConfigureMappings();
            var searchRepository = new SearchRepository(new SqlConnection("Data Source=localhost;Initial Catalog=InfoTrack;Integrated Security=True"));
            var articleRepository = new ArticleRepository(new SqlConnection("Data Source=localhost;Initial Catalog=InfoTrack;Integrated Security=True"));

            var search = new Search
            {
                SearchEngine = SearchEngine.Google,
                Query = args[0],
                Uri = uri
            };

            search = await searchRepository.Add(search);

            var scraper = new GoogleScraper(100);
            var scrapedArticles = await scraper.GetArticles(search.Query);

            for (int i = 0; i < scrapedArticles.Count; i++)
            {
                var scrapedUrl = scrapedArticles[i].SelectSingleNode(@"./a").Attributes[0].Value;
                var scrapedTitle = HttpUtility.HtmlDecode(scrapedArticles[i].InnerText);
                var baseUrl = search.Uri.Host[4..];

                if (!scrapedUrl.Contains(baseUrl, StringComparison.CurrentCultureIgnoreCase)) continue;

                var article = new Article
                {
                    Search = search,
                    Title = scrapedTitle,
                    Rank = i + 1,
                    Uri = new Uri(UriHelper.ToUri(scrapedUrl))
                };

                await articleRepository.Add(article);
            }
        }
    }
}
