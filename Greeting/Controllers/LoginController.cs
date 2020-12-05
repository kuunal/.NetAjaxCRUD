using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BusinessLayer;
using EmailService;
using Greeting.Constants;
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
                    return BadRequest(new Response<Employee>(null, (int)HttpStatusCode.BadRequest, ResponseMessages.INVALID_CREDENTIALS));
                }
                return Ok(new Response<Employee>(userData, (int)HttpStatusCode.OK, token));
            }
            catch (Exception e)
            {
                return BadRequest(new Response<Employee>(null, (int)HttpStatusCode.InternalServerError, e.Message));
            }

        }

        [HttpPost("{email}")]
        [Route("/forgot")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ForgotPassword([FromForm] string email)
        {
            try
            {
                Employee employee = await _service.SearchByEmail(email);
                if (employee != null)
                {
                    var currentUrl = HttpContext.Request.Host;
                    await _service.ForgotPassword(employee, currentUrl.Value);
                    return Ok(new Response<Employee>(null, (int)HttpStatusCode.OK, ResponseMessages.MAIL_SENT));
                }
                return BadRequest(new Response<Employee>(null, (int)HttpStatusCode.BadRequest, ResponseMessages.NO_SUCH_USER));
            }
            catch (Exception e)
            {
                return Json(new Response<Employee>(null, (int)HttpStatusCode.InternalServerError, e.Message));
            }
        }

        [HttpPost("{password, token}")]
        [Route("/Reset")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Reset([FromForm] string password, [FromForm] string token)
        {
            try
            {
                if (await _service.ResetPassword(password, token) == 1)
                    return Ok(new Response<Employee>(null, (int)HttpStatusCode.OK, ResponseMessages.PASSWORD_CHANGED));
                return BadRequest(new Response<Employee>(null, (int)HttpStatusCode.OK, ResponseMessages.FAILED));
            }
            catch (Exception e)
            {
                return Json(new Response<Employee>(null, (int)HttpStatusCode.InternalServerError, e.Message));
            }
        }
    }
}
