using ModelLayer;
using System.Security.Claims;

namespace TokenAuthentication
{
    public interface ITokenManager
    {
        string Encode(Employee employee);
        ClaimsPrincipal Decode(string token);
    }
}
