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
        public List<Room> Rooms { get; set; }
        public string Filtercriteria { get; set; }
        [BindProperty]
        public string SearchString { get; set; }

        public GetAllRoomsModel(IRoomCatalog service)
        {
            _service = service;
        }


        public async Task OnGetAsync()
        {
            Rooms = _service.GetAllRoomsAsync().Result;
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
