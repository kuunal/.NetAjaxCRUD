using Microsoft.Extensions.Configuration;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class Repository : IRepository<Employee>
    {
        private SqlConnection _conn;

        public Repository(BaseRepository baseRepository)
        {
            _conn = baseRepository.GetConnection();
        }

        public async Task<Employee> Add(Employee employeeData)
        {
            SqlCommand command = new SqlCommand("spEmployeeTable_Create");
            command.Parameters.AddWithValue("@name", employeeData.Name);
            command.Parameters.AddWithValue("@password", employeeData.Password);
            command.Parameters.AddWithValue("@address", employeeData.Address);
            command.Parameters.AddWithValue("@email", employeeData.Email);
            command.Parameters.AddWithValue("@phoneno", employeeData.PhoneNumber);
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@id";
            sqlParameter.SqlDbType = System.Data.SqlDbType.Int;
            sqlParameter.Direction = System.Data.ParameterDirection.Output;
            command.Parameters.Add(sqlParameter);
            await _conn.OpenAsync();
            command.Connection = _conn;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            employeeData.Id =  (int) await command.ExecuteScalarAsync();
            _conn.Close();
            return employeeData;
        }

        public async Task<List<Employee>> GetAsync()
        {
            List<Employee> employees = new List<Employee>();
            using (_conn) {
                await _conn.OpenAsync();
                SqlCommand command = new SqlCommand("spEmployees_GetEmployees", _conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        employees.Add(GetDataFromReader(reader));
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
                SqlCommand command = new SqlCommand("spEmployeeTable_GetById", _conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idInput", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        employee = GetDataFromReader(reader);
                    }
                }
                _conn.Close();
            }
            return employee;
        }

        public async Task<int> Remove(int id)
        {
            await _conn.OpenAsync();
            SqlCommand command = new SqlCommand("spEmployeeTable_Remove", _conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idInput", id);
            int result = await command.ExecuteNonQueryAsync(); 
            _conn.Close();
            return result;
        }

        public async Task<Employee> Update(int id, Employee employeeData)
        {
            SqlCommand command = new SqlCommand("spUpdateEmployee");
            Employee employee = null;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", employeeData.Name);
            command.Parameters.AddWithValue("@address", employeeData.Address);
            command.Parameters.AddWithValue("@phoneno", employeeData.PhoneNumber);
            command.Parameters.AddWithValue("@id", id);
            await _conn.OpenAsync();
            command.Connection = _conn;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (await reader.ReadAsync())
                {
                    employee = GetDataFromReader(reader);
                }
            }
            _conn.Close();
            return await GetAsync(id);
        }

        public Employee GetDataFromReader(SqlDataReader reader)
        {
           
            return new Employee
            {
                Id = Convert.ToInt32(reader["id"]),
                Name = (string)reader["name"],
                Email = (string)reader["email"],
                Password = (string)reader["password"],
                Address = (string)reader["address"],
                PhoneNumber = (long)reader["phoneno"]
            };

        }
    }
}
