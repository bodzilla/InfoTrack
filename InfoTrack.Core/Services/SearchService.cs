using System.Collections.Generic;
using System.Threading.Tasks;
using InfoTrack.Core.Contracts;
using InfoTrack.Core.Models;

namespace InfoTrack.Core.Services
{
    public sealed class SearchService : ISearchService
    {
        private readonly IRepository<Search> _searchRepository;

        public SearchService(IRepository<Search> searchRepository) => _searchRepository = searchRepository;

        /// <inheritdoc />
        public async Task<IEnumerable<Search>> GetAll()
        {
            var searches = await _searchRepository.GetAll();
            return searches;
        }

        /// <inheritdoc />
        public async Task<Search> Add(Search search)
        {
            var result = await _searchRepository.Add(search);
            return result;
        }
    }
}
