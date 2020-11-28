
using Greeting.DTOs.EmployeeDTO;
using Greeting.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greeting.Services
{
    public interface IService
    {
        Task<ServiceResponse<List<EmployeesDTO>>> GetEmployees();
        Task<ServiceResponse<EmployeesDTO>> GetEmployee(int id);
        Task<ServiceResponse<EmployeesDTO>> AddEmployee(EmployeesDTO employee);
        Task<ServiceResponse<EmployeesDTO>> RemoveEmployee(int id);
        Task<ServiceResponse<EmployeesDTO>> UpdateEmployee(int id, EmployeesDTO employee);


    }
}
