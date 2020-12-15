using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ModelLayer;

namespace RepositoryLayer
{
    public class LoginRepository : ILoginRepository<LoginDTO>
    {
        private SqlConnection _conn;
        public LoginRepository(BaseRepository baseRepository)
        {
            _conn = baseRepository.GetConnection();
        }

        public async Task<Employee> GetByEmail(string email)
        {
            Employee employee = null;
            using (_conn)
            {
                await _conn.OpenAsync();
                SqlCommand command = new SqlCommand("spEmployeeTable_GetByEmail", _conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@emailInput", email);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        employee = new Employee
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
                _conn.Close();
            }
            return employee;
        }


        public async Task<int> ResetPassword(string email, string password)
        {
            using (_conn)
            {
                await _conn.OpenAsync();
                SqlCommand command = new SqlCommand("update EmployeeTable set password = @password where email=@email", _conn);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);
                int result = await command.ExecuteNonQueryAsync();
                _conn.Close();
                return result;
            }
        }
    }
}
