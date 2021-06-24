using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.LuizaAuth.DTOs;
using Web.LuizaAuth.Interfaces;
using System.Threading.Tasks;

namespace Web.LuizaAuth.Pages.Account
{
    [AllowAnonymous]
    public class RecoveryPasswordModel : PageModel
    {
        private readonly IUserConsumerService _userConsumerService;

        public RecoveryPasswordModel(IUserConsumerService userConsumerService)
        {
            this._userConsumerService = userConsumerService;
        }

        [BindProperty]
        public RecoveryPasswordDto _recoveryPassword { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _userConsumerService.RecoveryPasswordAsync(_recoveryPassword);

            return RedirectToPage("/Index");
        }
    }
}
