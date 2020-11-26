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

        public IActionResult Get()
        {
            return Ok(_empService.GetEmployees());
        }

        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            return Ok(_empService.GetEmployee(id));
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeesDTO employee)
        {
            return Ok(_empService.AddEmployee(employee));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_empService.RemoveEmployee(id));
        }
    }
}
