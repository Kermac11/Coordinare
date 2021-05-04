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
    public class DetailsModel : PageModel
    {
        private IUserCatalog userCatalog;
        
        [BindProperty] public new User User { get; set; }

        public DetailsModel(IUserCatalog userCatalog)
        {
            this.userCatalog = userCatalog;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            User = await userCatalog.GetUserFromIdAsync((int)id);

            if (User == null)
                return NotFound();

            return Page();
        }

        //public IActionResult OnPostUpdateUser(int id)
        //{
        //    return RedirectToPage("/Users/Update", "UpdateUser", new { id = User.User_ID });
        //}
        public IActionResult OnPostChangePassword(int id)
        {
            return RedirectToPage("/Users/ChangePassword");
        }
        public IActionResult OnPostDeleteUser(int id)
        {
            return RedirectToPage("/Users/Delete");
        }
    }
}
