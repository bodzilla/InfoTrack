using System.Collections.Generic;
using System.Threading.Tasks;
using InfoTrack.Core.Models;

namespace InfoTrack.Core.Contracts
{
    public interface ISearchRepository
    {
        Task<IEnumerable<Search>> GetAll();

        Task<IEnumerable<Search>> GetAllWithChildren();

        Task<Search> Add(Search search);
    }
}
