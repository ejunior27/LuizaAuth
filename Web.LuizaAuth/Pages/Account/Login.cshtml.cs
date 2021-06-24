using Domain.LuizaAuth.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.LuizaAuth.Interfaces;
using Web.LuizaAuth.Services.Api;

namespace Web.LuizaAuth.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public UserAuthenticateRequestDto _credentials { get; set; }

        private readonly IUserConsumerService _userConsumerService;

        public LoginModel(IUserConsumerService userConsumerService)
        {
            this._userConsumerService = userConsumerService;
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            if (!ModelState.IsValid) return;
            
            var user = await _userConsumerService.LoginAsync(_credentials);

            if (user != null)
                await CreateSecurityContextAsync(user);
        }

        private async Task<IActionResult> CreateSecurityContextAsync(UserAuthenticateResponseDto response)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, response.User.Name),
                new Claim(ClaimTypes.Email, response.User.Email)
            };
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

            return RedirectToPage("/Index");
        }
    }
}
