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
        private IRoomCatalog _service;
        [BindProperty] public Room Room { get; set; }
        public string TextBox { get; set; }

        public DeleteModel(IRoomCatalog service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            TextBox = "Are you sure that you want to delete?";
            Room = await _service.GetRoomsFromIdAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        { 
            await _service.DeleteRoomAsync(Room.Room_ID);
            return RedirectToPage("GetAllRooms");
        }
    }
}
