
using Greeting.Models;
using System.Collections.Generic;

namespace Greeting.Services
{
    public interface IService
    {
        ServiceResponse<List<Employee>> GetEmployees();
        ServiceResponse<Employee> GetEmployee(int id);
        ServiceResponse<Employee> AddEmployee(Employee employee);

    }
}
