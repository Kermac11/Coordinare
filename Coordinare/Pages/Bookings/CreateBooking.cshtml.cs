using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Interfaces;
using Coordinare.Models;
using Coordinare.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Coordinare.Pages.Bookings
{
    public class CreateBookingModel : PageModel
    {
        private IBookingCatalog _service;
        private IEventCatalog _eventService;
        private IUserCatalog _userService;
        [BindProperty] public Booking CurrentBooking { get; set; }
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
            CurrentBooking = new Booking();
            CurrentBooking.User_ID = Uid;
            CurrentBooking.Event_ID = id;
            CurrentBooking.BookingDate = DateTime.Now;
            CurrentBooking.InWaitingList = false;

        }

        public async Task<IActionResult> OnPost()
        {
            if (await _service.CreateBooking(CurrentBooking) == true)
            {
                return RedirectToPage("/Events/GetAllEvents");
            }
            else
            {
                return Page();
            }
            
            
        }
    }
}
