
using Greeting.DTOs.EmployeeDTO;
using Greeting.Models;
using System.Collections.Generic;

namespace Greeting.Services
{
    public interface IService
    {
        ServiceResponse<List<EmployeesDTO>> GetEmployees();
        ServiceResponse<EmployeesDTO> GetEmployee(int id);
        ServiceResponse<EmployeesDTO> AddEmployee(EmployeesDTO employee);
        ServiceResponse<EmployeesDTO> RemoveEmployee(int id);
        //ServiceResponse<EmployeesDTO> UpdateEmployee(EmployeesDTO employee);


    }
}
