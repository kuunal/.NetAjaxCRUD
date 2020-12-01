
using ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IService
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);
        Task<Employee> AddEmployee(Employee employee);
        Task<int> RemoveEmployee(int id);
        Task<Employee> UpdateEmployee(int id, Employee employee);


    }
}
