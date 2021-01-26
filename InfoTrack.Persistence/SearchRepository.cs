using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using InfoTrack.Core.Contracts;
using InfoTrack.Core.Models;

namespace InfoTrack.Persistence
{
    public sealed class SearchRepository : RepositoryBase, ISearchRepository
    {
        private readonly IDbConnection _connection;

        /// <inheritdoc />
        public SearchRepository(IDbConnection connection) : base(connection) => _connection = connection;

        /// <inheritdoc />
        public async Task<IEnumerable<Search>> GetAll()
        {
            var result = await _connection.QueryAsync<Search>("SELECT [ID], [ENTITYCREATED], [SEARCHENGINE], [QUERY], [URL], [SCRAPEURL] FROM [DBO].[SEARCHES]");
            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Search>> GetAllWithChildren()
        {
            const string query = "SELECT [S].[ID], [S].[ENTITYCREATED], [S].[SEARCHENGINE], [S].[QUERY], [S].[URL], [S].[SCRAPEURL], [A].[ID], [A].[ENTITYCREATED], [A].[SEARCHID], [A].[TITLE], [A].RANK, [A].[URL] FROM [DBO].[SEARCHES] AS [S]LEFT JOIN [ARTICLES] AS [A]ON [A].[SEARCHID] = [S].[ID]";
            var result = await QueryWithChild<Search, Article, int>(query, x => x.Id, x => x.Articles);
            return result;
        }

        /// <inheritdoc />
        public async Task<Search> Add(Search search)
        {
            var dateTime = DateTime.Now;
            const string command = @"INSERT INTO [DBO].[SEARCHES] ([ENTITYCREATED], [SEARCHENGINE], [QUERY], [URL], [SCRAPEURL]) OUTPUT INSERTED.ID VALUES (@EntityCreated, @SearchEngine, @Query, @Url, @ScrapeUrl)";
            var id = await _connection.ExecuteScalarAsync<int>(command, new { EntityCreated = dateTime, SearchEngine = search.SearchEngine, Query = search.Query, Url = search.Url, ScrapeUrl = search.ScrapeUrl });
            search.Id = id;
            search.EntityCreated = dateTime;
            return search;
        }
    }
}
