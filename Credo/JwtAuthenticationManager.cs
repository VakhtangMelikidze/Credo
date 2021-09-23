using Credo.Data.Configuration;
using Credo.Data.Shared;
using Credo.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Credo
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly IConfiguration _configuration;
        private readonly string _key;

        public JwtAuthenticationManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = _configuration["tokenManagement:secret"];
        }

        public string Authenticate(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokeDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokeDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
