using Domain.LuizaAuth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.LuizaAuth.Interfaces
{
    public interface IUserConsumerService
    {
        Task<bool> CreateUserAsync(UserDto user);
        Task<UserAuthenticateResponseDto> LoginAsync(UserAuthenticateRequestDto user);
        Task RecoveryPasswordAsync(RecoveryPasswordDto user);
    }
}
