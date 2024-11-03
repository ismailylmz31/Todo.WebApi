using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Tokens.Configuration;
using Todo.Models.Entities;
using Todo.Models.Tokens;
using Todo.Service.Abstract;

namespace Todo.Service.Concretes
{
    public class JwtService : IJwtService
    {

        private readonly CustomTokenOptions _tokenOptions;
        private readonly UserManager<User> _userManager;
        public JwtService(IOptions<CustomTokenOptions> options, UserManager<User> userManager)
        {
            _tokenOptions = options.Value;
            _userManager = userManager;
        }
        public async Task<TokenResponseDto> CreateToken(User user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.GetSecurityKey(_tokenOptions.SecurityKey);

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                expires: accessTokenExpiration,
                claims: await GetClaims(user, _tokenOptions.Audience),
                signingCredentials: signingCredentials
                );

            var jwtHandler = new JwtSecurityTokenHandler();

            string token = jwtHandler.WriteToken(jwtSecurityToken);

            return new TokenResponseDto
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration
            };
        }

        private async Task<IEnumerable<Claim>> GetClaims(User user, List<string> audiences)
        {
            var userList = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim("email",user.Email),
            new Claim(ClaimTypes.Name, user.UserName),
        };

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count > 0)
            {
                userList.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
            }

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));


            return userList;
        }
    }
}
