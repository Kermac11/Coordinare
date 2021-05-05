using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coordinare.Exceptions;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coordinare.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly IUserCatalog userCatalog;
        [BindProperty] public User User { get; set; }
        public string InfoText { get; set; }
        public List<User> Users { get; private set; }
        public string RndPass { get; private set; }
        

        public CreateModel(IUserCatalog userCatalog)
        {
            this.userCatalog = userCatalog;
        }

        public async Task OnGetAsync()
        {
            InfoText = "Enter new user";
            RndPass = CreatePassword(5);
            Users = await userCatalog.GetAllUsersAsync();
        }
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder bld = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                bld.Append(valid[rnd.Next(valid.Length)]);
            }
            return bld.ToString();
        }

        public async Task<IActionResult> OnPostAsync(User user)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await userCatalog.CreateUserAsync(user);
                Users = await userCatalog.GetAllUsersAsync();
            }
            catch (ExistsException e)
            {
                InfoText = $"Something went wrong! {e.Message}";
                return Page();
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
