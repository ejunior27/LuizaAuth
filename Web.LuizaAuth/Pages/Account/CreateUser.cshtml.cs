using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.LuizaAuth.DTOs;
using Web.LuizaAuth.Interfaces;

namespace Web.LuizaAuth.Pages.Account
{
    [AllowAnonymous]
    public class CreateUserModel : PageModel
    {
        private readonly IUserConsumerService userConsumerService;

        public CreateUserModel(IUserConsumerService userConsumerService)
        {
            this.userConsumerService = userConsumerService;
        }

        [BindProperty]
        public UserDto _user { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await userConsumerService.CreateUserAsync(_user);

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
