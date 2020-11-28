using AutoMapper;
using EmailService;
using Greeting.DTOs.EmployeeDTO;
using Greeting.Models;
using Greeting.Repositories;
using Greeting.Services;
using Greeting.TokenAuthentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Greeting
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IService, EmployeeServices>();
            services.AddScoped<ILoginRepository<LoginDTO>, LoginRepository>();
            services.AddScoped<ILoginService<LoginDTO>, LoginService>();
            services.AddScoped<IRepository<Employee>, Repository>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddAutoMapper(typeof(Startup));
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseCors();
            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            );
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
