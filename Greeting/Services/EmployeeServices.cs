using AutoMapper;
using Greeting.DTOs.EmployeeDTO;
using Greeting.Models;
using Greeting.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greeting.Services
{
    public class EmployeeServices : IService
    {
        IMapper _mapper;
        IRepository<Employee> _repo;
        public EmployeeServices(IMapper mapper, IRepository<Employee> repo)
        { 
            _mapper = mapper;
            _repo = repo;
        }
        public ServiceResponse<EmployeesDTO> AddEmployee(EmployeesDTO employee)
        {
            ServiceResponse<EmployeesDTO> serviceResponse = new ServiceResponse<EmployeesDTO>();
            serviceResponse.Data = employee;
            _repo.Add(_mapper.Map<Employee>(employee));
            return serviceResponse;
        }

        public ServiceResponse<EmployeesDTO> GetEmployee(int id)
        {
            ServiceResponse<EmployeesDTO> serviceResponse = new ServiceResponse<EmployeesDTO>();
            serviceResponse.Data = _mapper.Map<EmployeesDTO>(_repo.Get(id));
            return serviceResponse;
        }

        
        public ServiceResponse<EmployeesDTO> RemoveEmployee(int id)
        {
            ServiceResponse<EmployeesDTO> serviceResponse = new ServiceResponse<EmployeesDTO>();
            _repo.Remove(id);
            serviceResponse.Message = "Deleted successfully"; 
            return serviceResponse;
        }

        public ServiceResponse<List<EmployeesDTO>> GetEmployees()
        {
            ServiceResponse<List<EmployeesDTO>> serviceResponse = new ServiceResponse<List<EmployeesDTO>>();
            List<Employee> employees = _repo.Get();
            serviceResponse.Data = employees.Select(employee => _mapper.Map<EmployeesDTO>(employee)).ToList();
            return serviceResponse; 
        }

        public ServiceResponse<EmployeesDTO> UpdateEmployee(EmployeesDTO employee)
        {
            throw new NotImplementedException();
        }
    }
}
