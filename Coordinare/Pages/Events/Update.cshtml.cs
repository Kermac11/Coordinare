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
        }
        public async void OnGet(int id)
        {
          Event = await _service.GetEventFromId(id);
          Rooms = _rservice.GetAllRoomsAsync().Result;
          Time = DateTime.UtcNow.ToString("yyyy-MM-ddT00:00");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            _service.UpdateEvent(Event,Event.Event_ID);
            return RedirectToPage("GetAllEvents");
        }
    }
}
