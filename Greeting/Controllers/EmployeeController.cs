using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greeting.DTOs.EmployeeDTO;
using Greeting.Models;
using Greeting.Services;
using Greeting.TokenAuthentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
        [TokenAuthenticationFilter]
        public async Task<IActionResult> GetEmployeeAsync()
        {
            return Ok(await _empService.GetEmployees());
        }

        [HttpGet("{id}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> DetailEmployeeAsync(int id)
        {
            return Ok(await _empService.GetEmployee(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync([FromForm] EmployeesDTO employee)
        {   
            return Ok(await _empService.AddEmployee(employee));
        }

        [HttpDelete("{id}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            return Ok(await _empService.RemoveEmployee(id));
        }

        [HttpPut("{id}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> EditEmployeeAsync(int id, [FromForm] EmployeesDTO updatedEmployee)
        {
            return Ok(await _empService.UpdateEmployee(id, updatedEmployee));
        }

    }
}
