using AutoMapper;
using ModelLayer;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
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
        public async Task<Employee> AddEmployee(Employee employee)
        {
            try { 
                return await _repo.Add(employee);
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Employee> GetEmployee(int id)
        {
            try {
                return await _repo.GetAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<int> RemoveEmployee(int id)
        {
            try { 
                return await _repo.Remove(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Employee>> GetEmployees()
        {
            try
            {
                return await _repo.GetAsync();
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Employee> UpdateEmployee(int id, Employee data)
        {
            try { 
                return (await _repo.Update(id, data));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
