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
        private readonly IUserCatalog userCatalog;
        [BindProperty(SupportsGet = true)] public User User { get; set; }
        public string InfoText { get; set; }
        public List<User> Users { get; private set; }

        public UpdateModel(IUserCatalog userCatalog)
        {
            this.userCatalog = userCatalog;
        }

        public async Task OnGetAsync(int id)
        {
            InfoText = "Enter new information";
            User = await userCatalog.GetUserFromIdAsync(id);
            Users = await userCatalog.GetAllUsersAsync();
        }

        public async Task<IActionResult> OnPostAsync(User user)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await userCatalog.UpdateUserAsync(user, user.User_ID);
                Users = await userCatalog.GetAllUsersAsync();
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
