using DAL.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

        public string GetAccessTokenString(User user)
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

        public string GetRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidAudience = tokenConfig.Value.ValidAudience,
                ValidIssuer = tokenConfig.Value.ValidIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Value.SymmetricSecurityKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }

            return principal;
        }
    }
}
