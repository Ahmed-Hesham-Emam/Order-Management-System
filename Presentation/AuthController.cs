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
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResultDto))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationErrorResponse))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
            {
            var result = await serviceManager.AuthService.LoginAsync(loginDto);
            return Ok(result);
            }

        [HttpPost("register")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResultDto))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationErrorResponse))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
        public async Task<IActionResult> Register(UserRegisterDto registerDto)
            {
            var result = await serviceManager.AuthService.RegisterAsync(registerDto);
            return Ok(result);
            }
        }
    }
