using Microsoft.Extensions.Configuration;
using Greeting.DTOs.EmployeeDTO;
using Greeting.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Greeting.Repositories
{
    public class LoginRepository : ILoginRepository<LoginDTO>
    {
        private IConfiguration _configuration;
        private string _connectionString;
        private SqlConnection _conn;

        public LoginRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("db1");
            _conn = new SqlConnection(_connectionString);
            Console.WriteLine(_connectionString);
        }

        
        public async Task<Employee> Login(LoginDTO user)
        {
            Employee employee = null;
            using (_conn)
            {
                await _conn.OpenAsync();
                SqlCommand command = new SqlCommand("Select id, name, address, email, password, phoneno from EmployeeTable where email=@email and password=@password", _conn);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@password", user.Password);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        employee = new Employee
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader.GetString(1),
                            Email = reader.GetString(3),
                            Password = reader.GetString(4),
                            Address = reader.GetString(2),
                            PhoneNumber = reader.GetInt32(5),
                        };
                    }
                }
                _conn.Close();
            }
            return employee;
        }

       
    }
}
