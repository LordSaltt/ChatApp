using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Models;

namespace API.Services
{
   public interface IAuthService
    {
        Task<User> Register(UserForRegisterDto user);
        Task<string> Login(UserForLoginDto userForLoginDto);
        Task<ICollection<User>> GetUsers();
    }
}