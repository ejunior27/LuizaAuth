using Domain.LuizaAuth.DTOs;
using Domain.LuizaAuth.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.LuizaAuth.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get()
        {
            return Ok(await this.userService.GetAsync());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] UserDto userViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await this.userService.PostAsync(userViewModel));
        }

        [HttpPost("recoverypassword")]
        public async Task<IActionResult> RecoveryPass([FromBody] RecoveryPasswordDto recoveryPasswordViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await this.userService.RecoveryPassword(recoveryPasswordViewModel);

            return Ok();
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticateRequestDto userViewModel) 
            => Ok(await userService.AuthenticateAsync(userViewModel));
    }
}
