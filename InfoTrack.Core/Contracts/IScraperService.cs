using System.Collections.Generic;
using System.Threading.Tasks;
using InfoTrack.Core.Models;

namespace InfoTrack.Core.Contracts
{
    public interface IScraperService
    {
        string ConstructScrapeUrl(string query);

        Task<IEnumerable<Article>> FindMatchingArticles(Search search);
    }
}
