using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace API.Dtos
{
    public class TokenHandlerDto
    {
        public JwtSecurityTokenHandler Handler { get; set; }
        public SecurityToken Token { get; set; }

    }
}