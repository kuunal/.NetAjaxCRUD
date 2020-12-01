using EmailService;
using ModelLayer;
using RepositoryLayer;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TokenAuthentication;

namespace BusinessLayer
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
        public async Task<(Employee, string)> AuthenticateEmployee(LoginDTO user)
        {
            try
            {
                Employee employee = await _repository.GetByEmail(user.Email);
                if (employee == null || !BCrypt.Net.BCrypt.Verify(user.Password, employee.Password.Trim()))
                {
                    return (null, null);
                }
                else
                {
                    string token = _tokenManager.Encode(employee);
                    return (employee, token);
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message); 
            }
        }

        public async Task<Employee> SearchByEmail(string email)
        {
            try { 
                return await _repository.GetByEmail(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task ForgotPassword(Employee employee, string currentUrl)
        {
            try { 
                string jwt = _tokenManager.Encode(employee);
                string url = "https://"+currentUrl+"/html/reset.html?"+jwt;
                Message message = new Message(new string[] { employee.Email },
                        "Password Reset Email",
                        $"<h6>Click on the link to reset password<h6><a href='{url}'>{jwt}</a>");
                await _emailSender.SendEmail(message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> ResetPassword(string password, string token)
        {
            try
            {
                ClaimsPrincipal claims = _tokenManager.Decode(token);
                var claim = claims.Claims.ToList();
                return (await _repository.ResetPassword(claim[0].Value, password));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
