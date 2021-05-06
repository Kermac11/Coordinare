using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Interfaces;
using Coordinare.Models;
using Coordinare.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coordinare.Pages.Bookings
{
    public class CreateBookingModel : PageModel
    {
        private IBookingCatalog _service;
        private IEventCatalog _eventService;
        private IUserCatalog _userService;
        public Event Event { get; set; }
        public User User { get; set; }
        public CreateBookingModel(IBookingCatalog service, IEventCatalog eventService, IUserCatalog userService)
        {
            _service = service;
            _eventService = eventService;
            _userService = userService;
        }
        public void OnGet(int id, int Uid)
        {
            Event = _eventService.GetEventFromId(id).Result;
            User = _userService.GetUserFromIdAsync(Uid).Result;
        }
    }
}
