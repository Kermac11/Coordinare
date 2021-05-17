using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Coordinare.Interfaces;
using Coordinare.Models;
using Coordinare.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coordinare.Pages.Users.Log
{
    public class LogInModel : PageModel
    {
        private readonly IUserCatalog catalog;
        private readonly LoginService loginService;
        public string AccessDenied = "";

        public LogInModel(IUserCatalog catalog, LoginService loginService)
        {
            this.catalog = catalog;
            this.loginService = loginService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public new User User { get; set; }

        public IActionResult OnPost()
        {
            foreach (User user in catalog.GetAllUsersAsync().Result)
            {
                if (user.Username == User.Username)
                {
                    if (user.Password == User.PasswordCheck)
                    {
                        TempData["Username"] = User.Username;

                        Response.Cookies.Append("UserId", $"{user.User_ID}");
                        loginService.UserLogin(user);
                        return RedirectToPage("/Index");
                    }
                }

                AccessDenied = "Username/Password doesn't exist";
            }

            return Page();
        }
    }
}
