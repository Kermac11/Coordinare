using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coordinare.Pages.Rooms
{
    public class DeleteModel : PageModel
    {
        private readonly IRoomCatalog _service;
        [BindProperty] public Room Room { get; set; }
        public string TextBox { get; set; }

        public DeleteModel(IRoomCatalog service)
        {
            _service = service;
        }

        public void OnGet(string id)
        {
            TextBox = "Are you sure that you want to delete?";
            Room = _service.GetRoomsFromIdAsync(id).Result;
        }

        public async Task<IActionResult> OnPosAsync()
        { 
            _service.DeleteRoomAsync(Room.Room_ID);
            return RedirectToPage("GetAllRooms", new { id = Room.Room_ID });
        }
    }
}
