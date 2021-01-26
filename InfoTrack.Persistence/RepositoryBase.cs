using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace InfoTrack.Persistence
{
    public abstract class RepositoryBase
    {
        private readonly IDbConnection _connection;

        protected RepositoryBase(IDbConnection connection) => _connection = connection;

        public async Task<IEnumerable<TParent>> QueryWithChild<TParent, TChild, TParentKey>(string query, Func<TParent, TParentKey> parentKeySelector, Func<TParent, IList<TChild>> childSelector)
        {
            var dictionary = new Dictionary<TParentKey, TParent>();
            TParent Map(TParent parent, TChild child)
            {
                if (!dictionary.ContainsKey(parentKeySelector(parent))) dictionary.Add(parentKeySelector(parent), parent);
                var parentSearch = dictionary[parentKeySelector(parent)];
                var childrenArticles = childSelector(parentSearch);
                if (child == null) return parentSearch;
                childrenArticles?.Add(child);
                return parentSearch;
            }
            await _connection.QueryAsync<TParent, TChild, TParent>(query, Map);
            return dictionary.Values;
        }
    }
}
