using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Greeting.CustomException
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAync(HttpContext context)
        {
            try
            {
                await _next(context);
            } 
            catch (Exception  ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(new GreetingException
            {
                Message = e.Message,
                StatusCode = context.Response.StatusCode
            }.ToString());
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
