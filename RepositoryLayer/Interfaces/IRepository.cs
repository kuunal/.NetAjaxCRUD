using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task<T> Add(T employeeData);

        Task<int> Remove(int id);
        Task<T> Update(int id, T employeeData);
    }
}
