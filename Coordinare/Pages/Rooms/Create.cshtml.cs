using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Exceptions;
using Coordinare.Interfaces;
using Coordinare.Models;
using Coordinare.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coordinare.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private readonly IRoomCatalog roomCatalog;
        [BindProperty] public Room Room { get; set; }
        List<Room> Rooms { get; set; }
        public string TextBox { get; set; }


        public CreateModel(IRoomCatalog RoomCatalog) => this.roomCatalog = RoomCatalog;

        public async Task OnGetAsync()
        {
            TextBox = "Create a new Room";
            Rooms = await roomCatalog.GetAllRoomsAsync();
        }

        public async Task<IActionResult> OnPost(Room room)
        {
            try
            {
                await roomCatalog.CreateRoomAsync(Room);
                Rooms = await roomCatalog.GetAllRoomsAsync();
            }
            catch (ExistsException e)
            {
                TextBox = $"Error! {e.Message}";
                return Page();
            }
            return RedirectToPage("GetAllRooms");
            // Skal vende tilbage til start siden når man har oprettet en bruger
        }
    }
}
