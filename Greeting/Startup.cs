using AutoMapper;
using BusinessLayer;
using EmailService;
using Greeting.TokenAuthentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ModelLayer;
using RepositoryLayer;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Data.SqlClient;
using TokenAuthentication;

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
            EmailConfiguration emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

            BaseRepository connectionString = Configuration
                .GetSection("ConnectionStrings")
                .Get<BaseRepository>();

            //services.Configure<BaseRepository>(Configuration.GetSection("myConfiguration"));
            services.AddSingleton(emailConfig);
            services.AddSingleton(connectionString);
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IService, EmployeeServices>();
            services.AddScoped<ILoginRepository<LoginDTO>, LoginRepository>();
            services.AddScoped<ILoginService<LoginDTO>, LoginService>();
            services.AddScoped<IRepository<Employee>, Repository>();
            services.AddSingleton<ITokenManager, TokenManager>();
            services.AddAutoMapper(typeof(Startup));
            services.AddCors();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Please insert JWT token into field"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
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
            app.UseSwagger();
            app.UseSwaggerUI(option=> {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "My API Version 1");
            });

            //app.ConfigureCustomExceptionMiddleware();
            //app.UseCors();

            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            );
            app.UseStaticFiles();
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
