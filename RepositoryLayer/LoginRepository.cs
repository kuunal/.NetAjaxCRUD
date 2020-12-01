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
                SqlCommand command = new SqlCommand("Select id, name, address, email, password, phoneno from EmployeeTable where email=@email", _conn);
                command.Parameters.AddWithValue("@email", email);
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
