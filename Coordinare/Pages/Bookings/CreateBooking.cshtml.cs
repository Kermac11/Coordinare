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
        private IRoomCatalog _Rservice;
        [BindProperty] public Booking CurrentBooking { get; set; }
        public Event Event { get; set; }
        [BindProperty] public User User { get; set; }
        public CreateBookingModel(IBookingCatalog service, IEventCatalog eventService, LoginService lService, IRoomCatalog Rservice)
        {
            _service = service;
            _eventService = eventService;
            _LService = lService;
            _Rservice = Rservice;
        }
        public void OnGet(int EventID)
        {
            Event = _eventService.GetEventFromId(EventID).Result;
            User = _LService.GetLoggedInUser();
            CurrentBooking = new Booking();
            CurrentBooking.User_ID = User.User_ID;
            CurrentBooking.Event_ID = EventID;
            CurrentBooking.BookingDate = DateTime.Now;
            if (_Rservice.GetRoomsFromIdAsync(Event.Room_ID).Result.Capacity - _service.GetBookingsFromEvent(EventID).Result.Count <= 0) 
            {
                CurrentBooking.InWaitingList = false;
            }
            else
            {
                CurrentBooking.InWaitingList = true;
            }

        }

        public async Task<IActionResult>OnPostBooking()
        {
            if (await _service.CreateBooking(CurrentBooking) == true)
            {
                return RedirectToPage("/Events/Index");
            }
            else
            {
                return Page();
            }
            
            
        }

        public async Task<IActionResult>OnPostReturn()
        {
            return  RedirectToPage("/Events/Index");
        }
    }
}
