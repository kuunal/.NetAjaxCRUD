using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BusinessLayer;
using Greeting.TokenAuthentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using Greeting.Constants;

namespace Greeting.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IService _empService;
        public EmployeeController(IService empService)
        {
            this._empService = empService;
        }

        [HttpGet]
        [Route("/Employee")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> GetEmployeeAsync()
        {
            try
            {
                List<Employee> employee = await _empService.GetEmployees();
                if (employee == null)
                {
                    return Ok(new Response<Employee>(null, (int)HttpStatusCode.BadRequest, ResponseMessages.NO_DATA_AVAILABLE));
                }
                return Ok(new Response<List<Employee>>(employee, (int)HttpStatusCode.OK, ResponseMessages.SUCCESSFULL));
            }
            catch (Exception e)
            {
                return BadRequest(new Response<Employee>(null, (int)HttpStatusCode.InternalServerError, e.Message));
            }
        }

        [HttpGet("{id}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> DetailEmployeeAsync(int id)
        {
            try
            {
                Employee employeeData = await _empService.GetEmployee(id);
                if (employeeData == null)
                {
                    return BadRequest(new Response<Employee>(employeeData, (int)HttpStatusCode.BadRequest, ResponseMessages.NO_SUCH_USER));
                }
                return Ok(new Response<Employee>(employeeData, (int)HttpStatusCode.OK, ResponseMessages.SUCCESSFULL));
            }
            catch (Exception e)
            {
                return BadRequest(new Response<Employee>(null, (int)HttpStatusCode.InternalServerError, e.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync([FromForm] Employee employee)
        {
            try
            {
                Employee addedEmployee = await _empService.AddEmployee(employee);
                if (addedEmployee == null)
                {
                    return BadRequest(new Response<Employee>(null, (int)HttpStatusCode.BadRequest, ResponseMessages.FAILED));
                }
                return Ok(new Response<Employee>(addedEmployee, (int)HttpStatusCode.OK, ResponseMessages.SUCCESSFULL));
            }
            catch (Exception e)
            {
                return BadRequest(new Response<Employee>(null, (int)HttpStatusCode.InternalServerError, e.Message));
            }
        }

        [HttpDelete("{id}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            try
            {
                int result = await _empService.RemoveEmployee(id);
                if (result == 0)
                {
                    return BadRequest(new Response<Employee>(null, (int)HttpStatusCode.BadRequest, ResponseMessages.NO_SUCH_USER));
                }
                return Ok(new Response<Employee>(null, (int)HttpStatusCode.OK, ResponseMessages.SUCCESSFULL));
            }
            catch (Exception e)
            {
                return BadRequest(new Response<Employee>(null, (int)HttpStatusCode.InternalServerError, e.Message));
            }
        }

        [HttpPut("{id}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> EditEmployeeAsync(int id, [FromForm] Employee updatedEmployee)
        {
            try
            {
                Employee employee = await _empService.UpdateEmployee(id, updatedEmployee);
                if (employee == null)
                {
                    return BadRequest(new Response<Employee>(null, (int)HttpStatusCode.BadRequest, ResponseMessages.NO_SUCH_USER));
                }
                return Ok(new Response<Employee>(employee, (int)HttpStatusCode.OK, ResponseMessages.SUCCESSFULL));
            }
            catch (Exception e)
            {
                return BadRequest(new Response<Employee>(null, (int)HttpStatusCode.InternalServerError, e.Message));
            }
        }

    }
}
