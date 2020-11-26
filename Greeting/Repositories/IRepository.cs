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
        List<T> Get();
        T Get(int id);
        void Add(T employeeData);

        void Remove(int id);
        T Update(int id);
    }
}
