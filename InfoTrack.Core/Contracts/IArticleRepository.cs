using System.Collections.Generic;
using System.Threading.Tasks;
using InfoTrack.Core.Models;

namespace InfoTrack.Core.Contracts
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAll();

        Task<IEnumerable<Article>> GetAllWithParent();

        Task<Article> Add(Article article);
    }
}
