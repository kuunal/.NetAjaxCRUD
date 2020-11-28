using Greeting.DTOs.EmployeeDTO;
using Greeting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Greeting.TokenAuthentication
{
    public interface ITokenManager
    {
        string Encode(Employee employee);
        ClaimsPrincipal Decode(string token);
    }
}
