using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService;
using Greeting.DTOs.EmployeeDTO;
using Greeting.Models;
using Greeting.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greeting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private ILoginService<LoginDTO> _service;

        public LoginController(ILoginService<LoginDTO> loginService)
        {
            _service = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateUser(LoginDTO user)
        {
            Employee employee = (await _service.SearchByEmail(user.Email)).Data;
            if (employee != null)
                return Ok(await _service.AuthenticateEmployee(user));
            return BadRequest(new { message = "Email Already exist!" });
        }

       
    }
}
