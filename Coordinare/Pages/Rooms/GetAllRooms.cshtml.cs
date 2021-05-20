using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coordinare.Pages.Rooms
{
    public class GetAllRoomsModel : PageModel
    {
        private IRoomCatalog _service;
        private IEventCatalog _catalog;
        public List<Room> Rooms { get; set; }
        [BindProperty]
        public string SearchString { get; set; }
        public Func<string, List<Event>> PlanEvent { get; set; }

        public GetAllRoomsModel(IRoomCatalog service, IEventCatalog catalog)
        {
            _service = service;
            _catalog = catalog;
        }


        public async Task OnGetAsync()
        {
            Rooms = _service.GetAllRoomsAsync().Result;
            PlanEvent = i =>
                _catalog.GetAllEvents().Result.FindAll(e => e.Room_ID == i);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!String.IsNullOrEmpty(SearchString))
            {
                Rooms = _service.GetAllRoomsAsync().Result.FindAll(r => r.Room_ID.ToLower().Contains(SearchString.ToLower()));
                return Page();
            }
            Rooms = _service.GetAllRoomsAsync().Result;
            return Page();

        }
    }
}
