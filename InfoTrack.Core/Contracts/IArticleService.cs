using System.Collections.Generic;
using System.Threading.Tasks;
using InfoTrack.Core.Models;

namespace InfoTrack.Core.Contracts
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAll();

        Task<Article> Add(Article article);
    }
}
