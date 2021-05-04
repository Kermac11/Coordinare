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
    public class DeleteModel : PageModel
    {
        private readonly IUserCatalog userCatalog;
        [BindProperty] public User User { get; set; }
        public string InfoText { get; set; }

        public DeleteModel(IUserCatalog userCatalog)
        {
            this.userCatalog = userCatalog;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            InfoText = "You are deleting a user! Be sure.";
            User = await userCatalog.GetUserFromIdAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await userCatalog.DeleteUserAsync(User.User_ID);
                
            }
            catch (Exception e)
            {
                InfoText = $"Something went wrong! {e.Message}";
                return Page();
            }
            return RedirectToPage("GetAllUsers");
        }
    }
}
