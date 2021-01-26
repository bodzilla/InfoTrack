using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using InfoTrack.Core.Contracts;
using InfoTrack.Core.Models;

namespace InfoTrack.Persistence
{
    public sealed class ArticleRepository : RepositoryBase, IArticleRepository
    {
        private readonly IDbConnection _connection;

        public ArticleRepository(IDbConnection connection) : base(connection) => _connection = connection;

        /// <inheritdoc />
        public async Task<IEnumerable<Article>> GetAll()
        {
            var result = await _connection.QueryAsync<Article>("SELECT [ID], [ENTITYCREATED], [SEARCHID], [TITLE], [RANK], [URL] FROM [DBO].[ARTICLES]");
            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Article>> GetAllWithParent()
        {
            const string query = "SELECT [A].[ID], [A].[ENTITYCREATED], [A].[SEARCHID], [A].[TITLE], [A].[RANK], [A].[URL], [S].[ID], [S].[ENTITYCREATED], [S].[SEARCHENGINE], [S].[QUERY], [S].[URL], [S].[SCRAPEURL] FROM [DBO].[ARTICLES] AS [A] INNER JOIN [SEARCHES] AS [S] ON [A].[SEARCHID] = [S].[ID]";

            static Article Map(Article article, Search search)
            {
                article.Search = search;
                return article;
            }

            var result = await _connection.QueryAsync<Article, Search, Article>(query, Map);
            return result;
        }

        /// <inheritdoc />
        public async Task<Article> Add(Article article)
        {
            var dateTime = DateTime.Now;
            const string command = @"INSERT INTO [DBO].[ARTICLES] ([ENTITYCREATED], [SEARCHID], [TITLE], [RANK], [URL]) OUTPUT INSERTED.ID VALUES (@EntityCreated, @SearchId, @Title, @Rank, @Url)";
            var id = await _connection.ExecuteScalarAsync<int>(command, new { EntityCreated = dateTime, SearchId = article.Search.Id, Title = article.Title, Rank = article.Rank, Url = article.Url.OriginalString });
            article.Id = id;
            article.EntityCreated = dateTime;
            return article;
        }
    }
}
