using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Exceptions;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coordinare.Pages.Users
{
    public class UpdateModel : PageModel
    {
        private readonly IUserCatalog UserCatalog;
        [BindProperty] public User User { get; set; }
        public string InfoText { get; set; }
        public List<User> Users { get; private set; }

        public UpdateModel(IUserCatalog userCatalog)
        {
            this.UserCatalog = userCatalog;
        }

        public async Task OnGetAsync()
        {
            InfoText = "Enter new user";
            Users = await UserCatalog.GetAllUsersAsync();
        }

        public async Task<IActionResult> OnPostAsync(User user)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await UserCatalog.UpdateUserAsync(user, user.User_ID);
                Users = await UserCatalog.GetAllUsersAsync();
            }
            catch (ExistsException e)
            {
                InfoText = $"Something went wrong! {e.Message}";
                return Page();
            }
            return RedirectToPage("GetAllUsers");
        }
    }
}
