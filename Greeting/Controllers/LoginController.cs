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

        [HttpPost("{email}")]
        [Route("/forgot")]
        public async Task<IActionResult> ForgotPassword([FromForm] string email)
        {
            Employee employee = (await _service.SearchByEmail(email)).Data;
            if (employee != null)
            {
                await _service.ForgotPassword(employee);
                return Ok(new { message = "Mail sent for resetting password" });
            }
            return BadRequest(new { error = "Email doenst exists" });
        }

        [HttpPost("{password, token}")]
        [Route("/Reset")]
        public async Task<IActionResult> Reset([FromForm] string password, [FromForm] string token)
        {
            if (await _service.ResetPassword(password, token) == 1)
                return Ok();
            return BadRequest();
        }
    }
}
