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
    public class GetAllUsersModel : PageModel
    {
        public List<User> Users { get; private set; }

        private IUserCatalog userCatalog;

        public GetAllUsersModel(IUserCatalog userCatalog)
        {
            this.userCatalog = userCatalog;
        }

        public async Task OnGetAsync()
        {
            Users = await userCatalog.GetAllUsersAsync();
        }
    }
}
