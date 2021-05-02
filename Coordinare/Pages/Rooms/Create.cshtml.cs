using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public CreateModel(IRoomCatalog RoomCatalog) => this.roomCatalog = RoomCatalog;

        public async Task OnGetAsync()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                await roomCatalog.CreateRoomAsync(Room);
                Rooms = await roomCatalog.GetAllRoomsAsync();
            }
            catch (Exception)
            {
                return Page();
            }
            // Skal vende tilbage til start siden når man har oprettet en bruger
            return RedirectToPage();
        }
    }
}
