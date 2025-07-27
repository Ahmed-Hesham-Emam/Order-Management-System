using Shared.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
    {
    public interface IAuthService
        {
        Task<TokenResponseDto> LoginAsync(UserLoginDto loginDto);
        Task<TokenResponseDto> RegisterAsync(UserRegisterDto registerDto);
        }
    }
