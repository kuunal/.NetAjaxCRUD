using AutoMapper;
using Greeting.DTOs.EmployeeDTO;
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
            new Employee{ Id=1, Name="f0rest", Email="f0rest@sk.com", address="NY", PhoneNumber=1234567890},
            new Employee{ Id=2, Name="GEt_Right", Email="Get_Right@sk.com", address="NY", PhoneNumber=1234567890},
            new Employee{ Id=3, Name="fallEn", Email="fallen@sk.com", address="NY", PhoneNumber=1234567890},
            new Employee{ Id=4, Name="Spawn", Email="Spawn@sk.com", address="NY", PhoneNumber=1234567890},
        };

        IMapper _mapper;
        public EmployeeServices(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ServiceResponse<EmployeesDTO> AddEmployee(EmployeesDTO employee)
        {
            ServiceResponse<EmployeesDTO> serviceResponse = new ServiceResponse<EmployeesDTO>();
            serviceResponse.Data = employee;
            empList.Add(_mapper.Map<Employee>(employee));
            return serviceResponse;
        }

        public ServiceResponse<EmployeesDTO> GetEmployee(int id)
        {
            ServiceResponse<EmployeesDTO> serviceResponse = new ServiceResponse<EmployeesDTO>();
            serviceResponse.Data = _mapper.Map<EmployeesDTO>(empList.FirstOrDefault(employee => employee.Id == id));
            return serviceResponse;
        }

        public ServiceResponse<List<EmployeesDTO>> GetEmployees()
        {
            ServiceResponse<List<EmployeesDTO>> serviceResponse = new ServiceResponse<List<EmployeesDTO>>();
            serviceResponse.Data = empList.Select(employee => _mapper.Map<EmployeesDTO>(employee)).ToList();
            return serviceResponse; 
        }
    }
}
