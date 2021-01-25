using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoTrack.Core.Contracts
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Add(T item);
    }
}
