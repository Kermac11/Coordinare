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
        [BindProperty(SupportsGet = true)] public User User { get; private set; }

        public DetailsModel(IUserCatalog userCatalog)
        {
            this.userCatalog = userCatalog;
        }
        public async Task OnGetAsync()
        {
            User = await userCatalog.GetUserFromIdAsync(User.User_ID);
        }
    }
}
