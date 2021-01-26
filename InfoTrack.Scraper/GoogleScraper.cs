using System.Threading.Tasks;
using HtmlAgilityPack;
using InfoTrack.Core.Contracts;

namespace InfoTrack.Scraper
{
    public sealed class GoogleScraper : IScraper
    {
        private readonly int _resultLimit;

        public GoogleScraper(int resultLimit) => _resultLimit = resultLimit;

        /// <inheritdoc />
        public string ConstructScrapeUrl(string query)
        {
            var formattedQuery = query.Replace(' ', '+');
            var url = $"http://www.google.co.uk/search?num={_resultLimit}&q={formattedQuery}";
            return url;
        }

        /// <inheritdoc />
        public async Task<HtmlNodeCollection> GetArticles(string query, string scrapeUrl)
        {
            var document = await new HtmlWeb().LoadFromWebAsync(scrapeUrl);
            var articles = document.DocumentNode.SelectNodes(@"//div[@class=""yuRUbf""]");
            return articles;
        }
    }
}