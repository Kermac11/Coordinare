using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coordinare.Pages.Bookings
{
    public class EventBookingsModel : PageModel
    {
        public List<Booking> bookings { get; private set; }

        [BindProperty] public int eventID { get; set; }

        private IBookingCatalog bookingCatalog;

        public EventBookingsModel(IBookingCatalog bService)
        {
            this.bookingCatalog = bService;
        }
        public async Task OnGetAsync(int Event_ID)
        {
           bookings = bookingCatalog.GetBookingsFromEvent(Event_ID).Result;
        }
    }
}

