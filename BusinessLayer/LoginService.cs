using EmailService;
using ModelLayer;
using RepositoryLayer;
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
            Employee employee = await _repository.Login(user);
            if (employee != null)
            {
                string token = _tokenManager.Encode(employee);
                return (employee,token);
            }
            return (null, null);
        }

        public async Task<Employee> SearchByEmail(string email)
        {
            return await _repository.GetByEmail(email);
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
