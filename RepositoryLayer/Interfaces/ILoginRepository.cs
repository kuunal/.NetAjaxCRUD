using ModelLayer;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public interface ILoginRepository<T>
    {
        Task<Employee> Login(T user);
        Task<Employee> GetByEmail(string email);

        Task<int> ResetPassword(string email, string password);

    }
}
