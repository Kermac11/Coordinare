using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coordinare.Pages.Admin
{
    public class AdminHubModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPostCreateEvent()
        {
            return RedirectToPage("/Events/Create");
        }
        public IActionResult OnPostCreateUser()
        {
            return RedirectToPage("/Users/Create");
        }
        public IActionResult OnPostCreateRoom()
        {
            return RedirectToPage("/Rooms/Create");
        }
        public IActionResult OnPostAllEvents()
        {
            return RedirectToPage("/Events/GetAllEvents");
        }
        public IActionResult OnPostAllUsers()
        {
            return RedirectToPage("/Users/GetAllUsers");
        }
        public IActionResult OnPostAllRooms()
        {
            return RedirectToPage("/Rooms/GetAllRooms");
        }
    }
}
