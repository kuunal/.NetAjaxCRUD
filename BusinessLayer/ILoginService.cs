using ModelLayer;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface ILoginService<T>
    {   
        Task<(Employee, string)> AuthenticateEmployee(T user);
        Task<Employee> SearchByEmail(string email);

        Task ForgotPassword(Employee employee, string currentUrl);
        Task<int> ResetPassword(string password, string token);

    }
}
