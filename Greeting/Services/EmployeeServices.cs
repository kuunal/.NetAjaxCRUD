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
        public async Task<ServiceResponse<EmployeesDTO>> AddEmployee(EmployeesDTO employee)
        {
            ServiceResponse<EmployeesDTO> serviceResponse = new ServiceResponse<EmployeesDTO>();
            serviceResponse.Data = employee;
            await _repo.Add(_mapper.Map<Employee>(employee));
            return serviceResponse;
        }

        public async Task<ServiceResponse<EmployeesDTO>> GetEmployee(int id)
        {
            ServiceResponse<EmployeesDTO> serviceResponse = new ServiceResponse<EmployeesDTO>();
            serviceResponse.Data = _mapper.Map<EmployeesDTO>(await _repo.GetAsync(id));
            return serviceResponse;
        }

        
        public async Task<ServiceResponse<EmployeesDTO>> RemoveEmployee(int id)
        {
            ServiceResponse<EmployeesDTO> serviceResponse = new ServiceResponse<EmployeesDTO>();
            await _repo.Remove(id);
            serviceResponse.Message = "Deleted successfully"; 
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<EmployeesDTO>>> GetEmployees()
        {
            ServiceResponse<List<EmployeesDTO>> serviceResponse = new ServiceResponse<List<EmployeesDTO>>();
            List<Employee> employees = await _repo.GetAsync();
            serviceResponse.Data = employees.Select(employee => _mapper.Map<EmployeesDTO>(employee)).ToList();
            return serviceResponse; 
        }

        public async Task<ServiceResponse<EmployeesDTO>> UpdateEmployee(EmployeesDTO employee)
        {
            throw new NotImplementedException();
        }
    }
}
