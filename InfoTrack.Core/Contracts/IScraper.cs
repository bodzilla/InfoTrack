using System.Threading.Tasks;
using HtmlAgilityPack;

namespace InfoTrack.Core.Contracts
{
    public interface IScraper
    {
        Task<HtmlNodeCollection> GetArticles(string query);
    }
}
