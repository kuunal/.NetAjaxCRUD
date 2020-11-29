using Greeting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greeting.Services
{
    public interface ILoginService<T>
    {   
        Task<ServiceResponse<Employee>> AuthenticateEmployee(T user);
        Task<ServiceResponse<Employee>> SearchByEmail(string email);

        Task ForgotPassword(Employee employee);
        Task<int> ResetPassword(string password, string token);

    }
}
