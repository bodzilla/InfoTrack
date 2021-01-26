﻿using System;
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
            var result = await _connection.QueryAsync<Search>("SELECT [ID], [ENTITYCREATED], [SEARCHENGINE], [QUERY], [URL] FROM [DBO].[SEARCHES]");
            return result;
        }

        /// <inheritdoc />
        public async Task<Search> Add(Search item)
        {
            var dateTime = DateTime.Now;
            const string command = @"INSERT INTO [DBO].[SEARCHES] ([ENTITYCREATED], [SEARCHENGINE], [QUERY], [URL]) OUTPUT INSERTED.ID VALUES (@EntityCreated, @SearchEngine, @Query, @Url)";
            var id = await _connection.ExecuteScalarAsync<int>(command, new { EntityCreated = dateTime, SearchEngine = item.SearchEngine, Query = item.Query, Url = item.Url });
            item.Id = id;
            item.EntityCreated = dateTime;
            return item;
        }
    }
}
