using Greeting.DTOs.EmployeeDTO;
using Greeting.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Greeting.Repositories
{
    public class Repository : IRepository<Employee>
    {
        private IConfiguration _configuration;
        private string _connectionString;
        private SqlConnection _conn;

        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("db1");
            _conn = new SqlConnection(_connectionString);
            Console.WriteLine(_connectionString);
        }

        public async Task Add(Employee employeeData)
        {
            SqlCommand command = new SqlCommand("insert into EmployeeTable(name, password, address, email, phoneno) " +
                "values(@name, @password, @address, @email, @phoneno)");
            command.Parameters.AddWithValue("@name", employeeData.Name);
            command.Parameters.AddWithValue("@password", employeeData.Password);
            command.Parameters.AddWithValue("@address", employeeData.Address);
            command.Parameters.AddWithValue("@email", employeeData.Email);
            command.Parameters.AddWithValue("@phoneno", employeeData.PhoneNumber);
            await _conn.OpenAsync();
            command.Connection = _conn;
            await command.ExecuteNonQueryAsync();
            _conn.Close();
        }

        public async Task<List<Employee>> GetAsync()
        {
            List<Employee> employees = new List<Employee>();
            using (_conn) {
                await _conn.OpenAsync();
                SqlCommand command = new SqlCommand("Select id, name, address, email, password, phoneno from EmployeeTable", _conn);
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        employees.Add(new Employee { Id = Convert.ToInt32(reader["id"]), 
                            Name = reader.GetString(1),
                            Email = reader.GetString(3),
                            Password = reader.GetString(4),
                            Address = reader.GetString(2),
                            PhoneNumber = reader.GetInt32(5),
                        });
                    }
                }
                _conn.Close();
            }
            return employees;
        }

        public async Task<Employee> GetAsync(int id)
        {
            Employee employee = null;
            using (_conn)
            {
                await _conn.OpenAsync();
                SqlCommand command = new SqlCommand("Select id, name, address, email, password, phoneno from EmployeeTable where id = @id", _conn);
                command.Parameters.AddWithValue("@id", id);
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

        public async Task Remove(int id)
        {
            await _conn.OpenAsync();
            SqlCommand command = new SqlCommand("Delete from EmployeeTable where id = @id", _conn);
            command.Parameters.AddWithValue("@id", id);
            await command.ExecuteNonQueryAsync();
            _conn.Close();
        }

        public async Task<Employee> Update(int id, Employee employeeData)
        {
            SqlCommand command = new SqlCommand("update EmployeeTable set name=@name, address=@address, phoneno=@phoneno where id = @id");
            command.Parameters.AddWithValue("@name", employeeData.Name);
            command.Parameters.AddWithValue("@address", employeeData.Address);
            command.Parameters.AddWithValue("@phoneno", employeeData.PhoneNumber);
            command.Parameters.AddWithValue("@id", id);
            await _conn.OpenAsync();
            command.Connection = _conn;
            await command.ExecuteNonQueryAsync();
            _conn.Close();
            return await GetAsync(id);
        }
    }
}
