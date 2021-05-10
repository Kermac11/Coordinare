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
        private LoginService _LService;
        [BindProperty] public Booking CurrentBooking { get; set; }
        public Event Event { get; set; }
        [BindProperty] public User User { get; set; }
        public CreateBookingModel(IBookingCatalog service, IEventCatalog eventService, LoginService lService)
        {
            _service = service;
            _eventService = eventService;
            _LService = lService;
        }
        public void OnGet(int EventID)
        {
            Event = _eventService.GetEventFromId(EventID).Result;
            User = _LService.GetLoggedInUser();
            CurrentBooking = new Booking();
            CurrentBooking.User_ID = User.User_ID;
            CurrentBooking.Event_ID = EventID;
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
