using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
    {
    [ApiController]
    [Route("api/users")]
    public class AuthController(IServiceManager serviceManager) : ControllerBase
        {
        [HttpPost("login")]

        public async Task<IActionResult> Login(UserLoginDto loginDto)
            {
            var result = await serviceManager.AuthService.LoginAsync(loginDto);
            return Ok(result);
            }

        [HttpPost("register")]

        public async Task<IActionResult> Register(UserRegisterDto registerDto)
            {
            var result = await serviceManager.AuthService.RegisterAsync(registerDto);
            return Ok(result);
            }
        }
    }
