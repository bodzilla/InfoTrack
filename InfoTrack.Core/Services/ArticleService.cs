using System.Collections.Generic;
using System.Threading.Tasks;
using InfoTrack.Core.Contracts;
using InfoTrack.Core.Models;

namespace InfoTrack.Core.Services
{
    public sealed class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _articleRepository;

        public ArticleService(IRepository<Article> articleRepository) => _articleRepository = articleRepository;

        /// <inheritdoc />
        public async Task<IEnumerable<Article>> GetAll()
        {
            var articles = await _articleRepository.GetAll();
            return articles;
        }

        /// <inheritdoc />
        public async Task<Article> Add(Article article)
        {
            var result = await _articleRepository.Add(article);
            return result;
        }
    }
}
