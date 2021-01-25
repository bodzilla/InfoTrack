using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using InfoTrack.Core.Contracts;
using InfoTrack.Core.Models;

namespace InfoTrack.Persistence
{
    public sealed class ArticleRepository : IRepository<Article>
    {
        private readonly DapperConfiguration _dapperConfiguration;
        private readonly IDbConnection _connection;

        public ArticleRepository(DapperConfiguration dapperConfiguration, IDbConnection connection)
        {
            _dapperConfiguration = dapperConfiguration;
            _connection = connection;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Article>> GetAll()
        {
            var result = await _connection.QueryAsync<Article>("SELECT [ID], [SEARCHID], [TITLE], [RANK], [URI] FROM [DBO].[ARTICLES]");
            return result;
        }

        /// <inheritdoc />
        public async Task<Article> Add(Article item)
        {
            const string command = @"INSERT INTO [DBO].[ARTICLES] ([SEARCHID], [TITLE], [RANK], [URI]) OUTPUT INSERTED.ID VALUES (@SearchId, @Title, @Rank, @Uri)";
            var id = await _connection.ExecuteScalarAsync<int>(command, new { SearchId = item.Search.Id, Title = item.Title, Rank = item.Rank, Uri = item.Uri.OriginalString });
            item.Id = id;
            return item;
        }
    }
}
