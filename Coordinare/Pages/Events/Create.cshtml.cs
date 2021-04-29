using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coordinare.Pages.Events
{
    public class CreateModel : PageModel
    {
        private IEventCatalog _eservice;
        private IRoomCatalog _rservice;
        [BindProperty] public Event Event { get; set; }

        public List<Room> Rooms { get; set; }
        public CreateModel(IEventCatalog eservice, IRoomCatalog rservice)
        {
            _eservice = eservice;
            _rservice = rservice;
        }

        public void OnGet()
        {
            Rooms = _rservice.GetAllRoomsAsync().Result;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _eservice.CreateEvent(Event);
            return RedirectToPage("GetAllEvents");
        }
    }
}
