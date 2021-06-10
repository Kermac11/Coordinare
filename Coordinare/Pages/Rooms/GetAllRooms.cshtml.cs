using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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
        
        public Func<string, List<Event>> PlanEvent { get; set; }
        public List<Room> Room { get; set; }
        [BindProperty] public string Currentfilter { get; set; }
        public string idSort { get; set; }
        public string csort { get; set; }
        public string CurrentSort { get; set; }
        public string esort { get; set; }
        

        public GetAllRoomsModel(IRoomCatalog service, IEventCatalog catalog)
        {
            _service = service;
            _catalog = catalog;
        }


        public async Task OnGetAsync(string sortOrder)
        {
            Rooms = _service.GetAllRoomsAsync().Result;

                PlanEvent = i =>
                    _catalog.GetAllEvents().Result.FindAll(r => r.Room_ID == i);
                

            idSort = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            csort = sortOrder == "Capacity" ? "capacity_desc" : "Capacity";
            
            switch (sortOrder)
            {
                case "id_desc":
                    Rooms.Sort((r1, r2) => r1.Room_ID.CompareTo(r2.Room_ID));
                    break;
                case "Capacity":
                    Rooms.Sort((c1, c2) => c2.Capacity.CompareTo(c1.Capacity));
                    break;
                case "capacity_desc":
                    Rooms.Sort((c1, c2) => c1.Capacity.CompareTo(c2.Capacity));
                    break;

                default:
                    Rooms.Sort((e1, e2) => this.PlanEvent(e1.Room_ID).Count.CompareTo(this.PlanEvent(e2.Room_ID).Count()));
                    break;
            }
           
        }
        

        public async Task<IActionResult> OnPostAsync(string SearchString)
        {

            Currentfilter = SearchString;

            if (!ModelState.IsValid)
            {
            }
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
