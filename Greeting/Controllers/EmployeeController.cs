using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greeting.DTOs.EmployeeDTO;
using Greeting.Models;
using Greeting.Services;
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

        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _empService.GetEmployees());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DetailAsync(int id)
        {
            return Ok(await _empService.GetEmployee(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync([FromForm] EmployeesDTO employee)
        {   
            return Ok(await _empService.AddEmployee(employee));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _empService.RemoveEmployee(id));
        }


    }
}
