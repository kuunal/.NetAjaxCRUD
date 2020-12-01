using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using EmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

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
        public async Task<IActionResult> AuthenticateUser([FromForm] LoginDTO user)
        {
            try { 
                var (userData, token) = await _service.AuthenticateEmployee(user);
                if (userData == null || token == null)
                {
                    return BadRequest(new ServiceResponse<Employee>(null, 400, "Invalid id or password"));
                }
                return Ok(new ServiceResponse<Employee>(userData, 200, token));
            }catch(Exception e)
            {
                return BadRequest(new ServiceResponse<Employee>(null, 400, e.Message));
            }

        }

        [HttpPost("{email}")]
        [Route("/forgot")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ForgotPassword([FromForm] string email)
        {
            Employee employee = await _service.SearchByEmail(email);
            if (employee != null)
            {
                var currentUrl = HttpContext.Request.Host;
                await _service.ForgotPassword(employee, currentUrl.Value);
                return Ok(new { message = "Mail sent for resetting password" , status=200});
            }
            return BadRequest(new { error = "Email doenst exists", status = 400 });
        }

        [HttpPost("{password, token}")]
        [Route("/Reset")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Reset([FromForm] string password, [FromForm] string token)
        {
            if (await _service.ResetPassword(password, token) == 1)
                return Ok(new { status = 200, message = "Password Reset successfull!"});
            return BadRequest(new { status = 400, message = "Password Reset Failed!" });
        }
    }
}
