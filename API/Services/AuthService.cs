using System.Threading.Tasks;
using API.Dtos;
using API.Models;
using API.Repositories;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace API.Services
{
    public class AuthService : IAuthService
    {
         private readonly IAuthRepository _repo;
         private readonly IConfiguration _config;
         private readonly IMapper _mapper;

        public AuthService(
            IAuthRepository repo, 
            IMapper mapper,
            IConfiguration config)
        {
            _config = config;
            _repo = repo;
             _mapper = mapper;
        }

        public async Task<ICollection<User>> GetUsers()
        {
             var users = await _repo.GetAll();
            return users.Select(c => _mapper.Map<User>(c)).ToList();
        }

        public async Task<string> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.UserName.ToLower(), userForLoginDto.Password);
            if (userFromRepo == null)
                return null;

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var response = new TokenHandlerDto();
            response.Handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            response.Token= response.Handler.CreateToken(tokenDescriptor);

            return response.Handler.WriteToken(response.Token);
        }

        public async Task<User> Register(UserForRegisterDto user)
        {
            user.UserName = user.UserName.ToLower();
            if (await _repo.UserExist(user.UserName))
                return null;

            var userToCreate = new User
            {
                UserName = user.UserName
            };

            return await _repo.Register(userToCreate, user.Password);   
        }
    }
}