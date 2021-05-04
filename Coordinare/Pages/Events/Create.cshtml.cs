using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coordinare.Pages.Events
{
    public class CreateModel : PageModel
    {
        private IEventCatalog _eservice;
        private IRoomCatalog _rservice;
        private IUserCatalog _uservice;
        [BindProperty] public Event Event { get; set; }
        public string Time { get; set; }

        public List<Room> Rooms { get; set; }
        public List<User> Speakers { get; set; }
        [BindProperty]
        public TimeSpan dura { get; set; }
        public CreateModel(IEventCatalog eservice, IRoomCatalog rservice, IUserCatalog uservice)
        {
            _eservice = eservice;
            _rservice = rservice;
            Rooms = _rservice.GetAllRoomsAsync().Result;
            Speakers = uservice.GetAllUsersAsync().Result.FindAll(u => u.Speaker == true);
        }

        public void OnGet()
        {

            Time = DateTime.UtcNow.ToString("yyyy-MM-ddT00:00");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();

            }
            _eservice.CreateEvent(Event);
            return RedirectToPage("GetAllEvents");

        }
    }
}
