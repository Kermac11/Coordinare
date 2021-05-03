using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Coordinare.Interfaces;
using Coordinare.Models;

namespace Coordinare.Pages.Events
{
    public class UpdateModel : PageModel
    {
        private IEventCatalog _service;
        private IRoomCatalog _rservice;
        [BindProperty]
        public Event Event { get; set; }
        public List<Room> Rooms { get; set; }
        public string Time { get; set; }
        public UpdateModel(IEventCatalog service, IRoomCatalog rservice)
        {
            _service = service;
            _rservice = rservice;
            Rooms = _rservice.GetAllRoomsAsync().Result;
        }
        public async void OnGet(int id)
        {
          Event = await _service.GetEventFromId(id);
          Time = DateTime.UtcNow.ToString("yyyy-MM-ddT00:00");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _service.UpdateEvent(Event, Event.Event_ID);
            return RedirectToPage("GetAllEvents");
            
        }
    }
}
