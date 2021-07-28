using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using DAL.Models;
using Microsoft.Extensions.Options;
using WebAPI.Configs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IOptions<TokenConfig> tokenConfig;

        public AuthController(IUserRepository repository, IOptions<TokenConfig> config)
        {
            userRepository = repository;
            tokenConfig = config;
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest();
            }
            User user = userRepository.FindUserByLoginModel(loginModel);
            if (user != null)
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
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
