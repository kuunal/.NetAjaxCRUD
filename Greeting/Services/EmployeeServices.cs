using Greeting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greeting.Services
{
    public class EmployeeServices : IService
    {
        List<Employee> empList = new List<Employee>() {
            new Employee(),
            new Employee{ Id=2, Name="GEt_Right", Email="Get_Right@sk.com"},
            new Employee{ Id=3, Name="fallEn", Email="fallen@sk.com"},
            new Employee{ Id=4, Name="Spawn", Email="Spawn@sk.com"},
        };
        public ServiceResponse<Employee> AddEmployee(Employee employee)
        {
            ServiceResponse<Employee> serviceResponse = new ServiceResponse<Employee>();
            serviceResponse.Data = employee;
            empList.Add(employee);
            return serviceResponse;
        }

        public ServiceResponse<Employee> GetEmployee(int id)
        {
            ServiceResponse<Employee> serviceResponse = new ServiceResponse<Employee>();
            serviceResponse.Data = empList.FirstOrDefault(employee => employee.Id == id);
            return serviceResponse;
        }

        public ServiceResponse<List<Employee>> GetEmployees()
        {
            ServiceResponse<List<Employee>> serviceResponse = new ServiceResponse<List<Employee>>();
            serviceResponse.Data = empList;
            return serviceResponse; 
        }
    }
}
