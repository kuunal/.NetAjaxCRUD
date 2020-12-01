using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Greeting.TokenAuthentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace Greeting.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private IService _empService;
        public EmployeeController(IService empService)
        {
            this._empService = empService;
        }

        [HttpGet]
        [Route("/Employee")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> GetEmployeeAsync()
        {
            try {
                List<Employee> employee = await _empService.GetEmployees();
                if (employee == null)
                {
                    return Ok(new ServiceResponse<Employee>(null, 200, "No data!"));
                }
                    return Ok(new ServiceResponse<List<Employee>>(employee , 200, "Successful!"));
            }catch(Exception e)
            {
                return BadRequest(new ServiceResponse<Employee>(null, 500, e.Message));
            }
        }

        [HttpGet("{id}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> DetailEmployeeAsync(int id)
        {
            try {
                Employee employeeData = await _empService.GetEmployee(id);
                if (employeeData == null)
                {
                    return BadRequest(new ServiceResponse<Employee>(employeeData, 400, "NO such user"));
                }
                return Ok(new ServiceResponse<Employee>(employeeData, 200, "Successfull"));
            }catch(Exception e)
            {
                return BadRequest(new ServiceResponse<Employee>(null, 500, e.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync([FromForm] Employee employee)
        {
            try {
                Employee addedEmployee = await _empService.AddEmployee(employee);
                if (addedEmployee == null)
                {
                    return BadRequest(new ServiceResponse<Employee>(null, 400, "Failed to add record"));
                }
                return Ok(new ServiceResponse<Employee>(addedEmployee, 200, "Addded Successfully"));
            }
            catch(Exception e)
            {
                return BadRequest(new ServiceResponse<Employee>(null, 500, e.Message));
            }
        }

        [HttpDelete("{id}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            try { 
                int result = await _empService.RemoveEmployee(id);
                if (result == 0)
                {
                    return BadRequest(new ServiceResponse<Employee>(null, 400, "Failed to Delete"));
                }
                return Ok(new ServiceResponse<Employee>(null, 200, "Deleted Successfully"));
            }catch(Exception e)
            {
                return BadRequest(new ServiceResponse<Employee>(null, 500, e.Message));
            }
        }

        [HttpPut("{id}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> EditEmployeeAsync(int id, [FromForm] Employee updatedEmployee)
        {
            try { 
                Employee employee = await _empService.UpdateEmployee(id, updatedEmployee);
                if (employee == null)
                {
                    return BadRequest(new ServiceResponse<Employee>(null, 400, "Failed to Update"));
                }
                    return Ok(new ServiceResponse<Employee>(employee, 200, "Updated employee data"));
            }
            catch (Exception e)
            {
                return BadRequest(new ServiceResponse<Employee>(null, 500, e.Message));
            }
        }

    }
}
