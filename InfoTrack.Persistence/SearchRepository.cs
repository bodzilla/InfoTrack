using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using InfoTrack.Core.Contracts;
using InfoTrack.Core.Models;

namespace InfoTrack.Persistence
{
    public sealed class SearchRepository : IRepository<Search>
    {
        private readonly IDbConnection _connection;

        public SearchRepository(IDbConnection connection) => _connection = connection;

        /// <inheritdoc />
        public async Task<IEnumerable<Search>> GetAll()
        {
            var result = await _connection.QueryAsync<Search>("SELECT [ID], [SEARCHENGINE], [QUERY], [URI] FROM [DBO].[SEARCHES]");
            return result;
        }

        /// <inheritdoc />
        public async Task<Search> Add(Search item)
        {
            const string command = @"INSERT INTO [DBO].[SEARCHES] ([SEARCHENGINE], [QUERY], [URI]) OUTPUT INSERTED.ID VALUES (@SearchEngine, @Query, @Uri)";
            var id = await _connection.ExecuteScalarAsync<int>(command, new { SearchEngine = item.SearchEngine, Query = item.Query, Uri = item.Uri.OriginalString });
            item.Id = id;
            return item;
        }
    }
}
