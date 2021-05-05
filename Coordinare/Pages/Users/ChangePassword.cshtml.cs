using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coordinare.Pages.Users
{
    public class ChangePasswordModel : PageModel
    {
        private IUserCatalog userCatalog;
        private string _newPassword;
        public string AccessDenied = "";

        public ChangePasswordModel(IUserCatalog userCatalog)
        {
            this.userCatalog = userCatalog;
        }

        [BindProperty]
        public new User User { get; set; }

        public IActionResult OnPost(int? id)
        {
            if (userCatalog.GetUserFromIdAsync((int)id).Result.Password == User.PasswordCheck)
            {
                _newPassword = User.Password;
                User = userCatalog.GetUserFromIdAsync((int)id).Result;
                User.Password = _newPassword;

                userCatalog.UpdateUserAsync(User, (int)id);
                return RedirectToPage("/Users/Details", new { id = id });
            }

            AccessDenied = "Wrong password";
            return Page();
        }
    }
}
