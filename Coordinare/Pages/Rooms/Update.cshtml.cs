using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Exceptions;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coordinare.Pages.Rooms
{
    public class UpdateModel : PageModel
    {

        private IRoomCatalog _service;
        [BindProperty] public Room Room { get; set; }
        public string TextBox { get; set; }
        public List<Room> Rooms { get; set; }

        public UpdateModel(IRoomCatalog service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            TextBox = "Update a Room?";
            Room = await _service.GetRoomsFromIdAsync(id);
            Rooms = await _service.GetAllRoomsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Room room)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await _service.UpdateRoomAsync(Room, Room.Room_ID);
                Rooms = await _service.GetAllRoomsAsync();
            }
            catch (ExistsException e)
            {
                TextBox = $"Error! {e.Message}";
                return Page();
            }
            return RedirectToPage("GetAllRooms");
            //await _service.UpdateRoomAsync(Room.Room_ID);
            //return RedirectToPage("GetAllRooms");
        }
    }
}

