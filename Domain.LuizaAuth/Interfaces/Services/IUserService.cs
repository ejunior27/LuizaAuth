using Domain.LuizaAuth.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.LuizaAuth.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAsync();
        Task<bool> PostAsync(UserDto userViewModel);        
        Task<UserAuthenticateResponseDto> AuthenticateAsync(UserAuthenticateRequestDto user);
        Task RecoveryPassword(RecoveryPasswordDto recoveryPasswordViewModel);
    }
}
