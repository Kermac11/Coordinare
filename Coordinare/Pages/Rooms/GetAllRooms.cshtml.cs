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
    public class GetAllRoomsModel : PageModel
    {
        private IRoomCatalog _service;
        public List<Room> Rooms { get; set; }

        public GetAllRoomsModel(IRoomCatalog service)
        {
            _service = service;
        }
            
        
        public async void OnGetAsync()
        {
            Rooms =  _service.GetAllRoomsAsync().Result;
        }
    }
}
