﻿using Microsoft.Extensions.Options;
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

        public string GetTokenString()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Value.SymmetricSecurityKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: tokenConfig.Value.ValidIssuer,
                audience: tokenConfig.Value.ValidAudience,
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
