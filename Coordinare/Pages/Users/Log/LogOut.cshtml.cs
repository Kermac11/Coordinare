using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coordinare.Pages.Users.Log
{
    public class LogOutModel : PageModel
    {
        private readonly LoginService logInService;

        public LogOutModel(LoginService logInService)
        {
            this.logInService = logInService;
        }

        public IActionResult OnGet()
        {
            logInService.UserLogout();

            return RedirectToPage("/Index");
        }
    }
}
