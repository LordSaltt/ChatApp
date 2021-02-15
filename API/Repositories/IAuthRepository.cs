using System.Threading.Tasks;
using API.Models;

namespace API.Repositories
{
    public interface IAuthRepository : IRepository<User>
    {
        Task<User> Register(User user, string password);
         Task<User> Login(string user, string password);
         Task<bool> UserExist(string username);

    }
}