using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Coordinare.Interfaces;
using Coordinare.Models;
using Coordinare.Services;

namespace Coordinare.Pages.Events
{
    public class InfoModel : PageModel
    {
        private IEventCatalog _service;
        private IBookingCatalog _Bservice;
        private LoginService _Lservice;
        public List<Booking> UserBookings;
        public Event Event { get; set; }
        public User CurrentUser { get; set; }

        [BindProperty] public int EventID { get; set; }

        public InfoModel(IEventCatalog service, IBookingCatalog Bservice, LoginService Lservice)
        {
            _service = service;
            _Bservice = Bservice;
            _Lservice = Lservice;
        }
        public void OnGet(int id)
        {
          Event = _service.GetEventFromId(id).Result;
          EventID = id;
          CurrentUser = _Lservice.GetLoggedInUser();
          UserBookings = _Bservice.GetBookingsFromUser(CurrentUser.User_ID).Result;
        }

        public async Task<IActionResult> OnPost()
        {
            return RedirectToPage("/Bookings/CreateBooking", new { EventID = this.EventID });
        }
    }
}
