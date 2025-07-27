using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared;
using Shared.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
    {
    public class AuthService(SignInManager<User> signInManager, UserManager<User> userManager, IOptions<JwtOptions> options) : IAuthService
        {
        public async Task<TokenResponseDto> LoginAsync(UserLoginDto loginDto)
            {
            var user = await userManager.FindByNameAsync(loginDto.UserName);

            if ( user is null ) throw new Exception("User not found");

            var flag = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if ( !flag.Succeeded ) throw new Exception("Invalid password");

            return new TokenResponseDto
                {
                AccessToken = await GenerateJwtToken(user),
                };
            }

        public async Task<TokenResponseDto> RegisterAsync(UserRegisterDto registerDto)
            {
            var user = new User
                {
                UserName = registerDto.UserName,
                Email = registerDto.Email,

                };
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if ( !result.Succeeded )
                {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }

            //await userManager.AddToRoleAsync(user, "Customer");

            return new TokenResponseDto
                {
                AccessToken = await GenerateJwtToken(user),
                };

            }


        private async Task<string> GenerateJwtToken(User user)
            {

            var jwtOptions = options.Value;
            var authClaims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, user.UserName),
                };
            var roles = await userManager.GetRolesAsync(user);
            foreach ( var role in roles )
                {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                expires: DateTime.UtcNow.AddDays(jwtOptions.ExpireTimeDays),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }
