using DAL.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Configs;

namespace WebAPI.Services
{
    public class AuthService
    {
        private readonly IOptions<TokenConfig> tokenConfig;

        public AuthService(IOptions<TokenConfig> config)
        {
            tokenConfig = config;
        }

        public string GetTokenString(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Value.SymmetricSecurityKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var tokenOptions = new JwtSecurityToken(
                issuer: tokenConfig.Value.ValidIssuer,
                audience: tokenConfig.Value.ValidAudience,
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public bool IsTokenExpired(string tokenString)
        {
            if (string.IsNullOrEmpty(tokenString))
            {
                return true;
            }

            var jwtToken = new JwtSecurityToken(tokenString);
            return (jwtToken == null) || (jwtToken.ValidFrom > DateTime.UtcNow) || (jwtToken.ValidTo < DateTime.UtcNow);
        }
    }
}
