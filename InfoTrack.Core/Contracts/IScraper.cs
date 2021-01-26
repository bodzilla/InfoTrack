using System.Threading.Tasks;
using HtmlAgilityPack;

namespace InfoTrack.Core.Contracts
{
    public interface IScraper
    {
        string ConstructScrapeUrl(string query);

        Task<HtmlNodeCollection> GetArticles(string query, string scrapeUrl);
    }
}
