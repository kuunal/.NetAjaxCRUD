using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greeting.TokenAuthentication
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var _tokenManager = (ITokenManager) context.HttpContext.RequestServices.GetService(typeof(ITokenManager));

            if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.HttpContext.Request.Headers.First(cookie=> cookie.Key == "Authorization").Value;
                try
                {
                    var claimPrinciple = _tokenManager.Decode(token);
                }catch(Exception)
                {
                    context.ModelState.AddModelError("Unauthorized", "Invalid token");
                }
            }
            else
            {
                context.ModelState.AddModelError("Unauthorized", "Missing token");
            }
        }
    }
}
