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

        public void Add(Employee employeeData)
        {
            SqlCommand command = new SqlCommand("insert into EmployeeTable(name, password, address, email, phoneno) values(@name, @password, @address, @email, @phoneno)");
            command.Parameters.AddWithValue("@name", employeeData.Name);
            command.Parameters.AddWithValue("@password", employeeData.Password);
            command.Parameters.AddWithValue("@address", employeeData.Address);
            command.Parameters.AddWithValue("@email", employeeData.Email);
            command.Parameters.AddWithValue("@phoneno", employeeData.PhoneNumber);
            _conn.Open();
            command.Connection = _conn;
            command.ExecuteNonQuery();
            _conn.Close();
        }

        public List<Employee> Get()
        {
            List<Employee> employees = new List<Employee>();
            using (_conn) {
                _conn.Open();
                SqlCommand command = new SqlCommand("Select id, name, address, email, password, phoneno from EmployeeTable", _conn);
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
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

        public Employee Get(int id)
        {
            Employee employee = null;
            using (_conn)
            {
                _conn.Open();
                SqlCommand command = new SqlCommand("Select id, name, address, email, password, phoneno from EmployeeTable where id = @id", _conn);
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
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

        public void Remove(int id)
        {
            _conn.Open();
            SqlCommand command = new SqlCommand("Delete from EmployeeTable where id = @id", _conn);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            _conn.Close();
        }

        public Employee Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
