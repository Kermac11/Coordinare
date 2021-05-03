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
        [BindProperty] public Event Event { get; set; }
        public string Time { get; set; }

        public List<Room> Rooms { get; set; }
        public string Message { get; set; }
        public CreateModel(IEventCatalog eservice, IRoomCatalog rservice)
        {
            _eservice = eservice;
            _rservice = rservice;
            Rooms = _rservice.GetAllRoomsAsync().Result;
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
