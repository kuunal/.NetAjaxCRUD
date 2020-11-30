using EmailService;
using Greeting.DTOs.EmployeeDTO;
using Greeting.Models;
using Greeting.Repositories;
using Greeting.TokenAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Greeting.Services
{
    public class LoginService : ILoginService<LoginDTO>
    {
        ILoginRepository<LoginDTO> _repository;
        private IEmailSender _emailSender;
        ITokenManager _tokenManager;
        public LoginService(ILoginRepository<LoginDTO> loginRepository
            , ITokenManager tokenManager
            , IEmailSender emailSender)
        {
            _repository = loginRepository;
            _emailSender = emailSender;
            _tokenManager = tokenManager;
        }
        public async Task<ServiceResponse<Employee>> AuthenticateEmployee(LoginDTO user)
        {
            ServiceResponse<Employee> response = new ServiceResponse<Employee>();
            Employee employee = await _repository.Login(user);
            if (employee == null)
            {
                response.Message = "Login failed! Please check id or password";
                response.Success = false;
                return response;
            }
            response.Data = employee;
            response.Message = _tokenManager.Encode(employee);
            return response;
        }

        public async Task<ServiceResponse<Employee>> SearchByEmail(string email)
        {
            ServiceResponse<Employee> response = new ServiceResponse<Employee>();
            response.Data = await _repository.GetByEmail(email);
            return response;
        }

        public async Task ForgotPassword(Employee employee, string currentUrl)
        {
            string jwt = _tokenManager.Encode(employee);
            string url = "https://"+currentUrl+"/html/reset.html?"+jwt;
            Message message = new Message(new string[] { employee.Email },
                    "Password Reset Email",
                    $"<h6>Click on the link to reset password<h6><a href='{url}'>{jwt}</a>");
            await _emailSender.SendEmail(message);
        }

        public async Task<int> ResetPassword(string password, string token)
        {
            ClaimsPrincipal claims = _tokenManager.Decode(token);
            var claim =  claims.Claims.ToList();
            //claim;
            return (await _repository.ResetPassword(claim[0].Value, password));
        }
    }
}
