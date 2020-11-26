using Greeting.DTOs.EmployeeDTO;
using Greeting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greeting.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task Add(T employeeData);

        Task Remove(int id);
        Task<T> Update(int id);
    }
}
