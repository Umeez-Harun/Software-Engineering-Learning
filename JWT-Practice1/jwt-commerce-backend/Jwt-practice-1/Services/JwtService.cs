using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ServiceContracts.DTO;
using Entities;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security;

namespace Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthenticationResponse generateToken(ApplicationUser user, string role)
        {
            //1. retrieve the expiration time for the token
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:EXPIRATION_MINUTES"]!));

            //2. Set-up the claims
            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.name),
                new Claim(ClaimTypes.Role, role)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken configuration = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expirationTime,
                signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token = handler.WriteToken(configuration);

            return new AuthenticationResponse() { email = user.Email!, name = user.name, token =  token, expiresAt = expirationTime };
        }

        public string generateRefreshToken()
        {
            Byte[] bytes = new byte[64];
            RandomNumberGenerator generator = RandomNumberGenerator.Create();
            generator.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }

        public ClaimsPrincipal getPrincipal(string token)
        {
            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],

                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),

                ValidateLifetime = false,
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = handler.ValidateToken(token,parameters, out SecurityToken validatedToken);

            if(validatedToken is not JwtSecurityToken s_token || !s_token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityException("Token is not a JWT Token");
            }
            return principal;
        }
    }
}
