using System.Collections.Generic;
using System.Threading.Tasks;
using InfoTrack.Core.Models;

namespace InfoTrack.Core.Contracts
{
    public interface IScraperService
    {
        Task<IEnumerable<Article>> FindArticleMatches(Search search);
    }
}
