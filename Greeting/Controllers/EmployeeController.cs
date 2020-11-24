using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greeting.Models;
using Microsoft.AspNetCore.Mvc;

namespace Greeting.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        List<Employee> empList = new List<Employee>() {
            new Employee(),
            new Employee{ Id=2, Name="GEt_Right", Email="Get_Right@sk.com"},
            new Employee{ Id=3, Name="fallEn", Email="fallen@sk.com"},
            new Employee{ Id=4, Name="Spawn", Email="Spawn@sk.com"},
        };
        public IActionResult Get()
        {
            return Ok(empList);
        }

        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            return Ok(empList.FirstOrDefault(employee => employee.Id == id));
        }


        public IActionResult AddEmployee(Employee employee)
        {
            empList.Add(employee);
            return Ok(employee)
        }
    }
}
