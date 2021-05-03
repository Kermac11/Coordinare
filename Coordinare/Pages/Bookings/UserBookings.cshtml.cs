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
    public class UserBookingsModel : PageModel
    {
        public List<Booking> bookings { get; private set; }

        [BindProperty] public int userID { get; set; }

        private IBookingCatalog bookingCatalog;

        public UserBookingsModel(IBookingCatalog bService)
        {
            this.bookingCatalog = bService;
        }
        public async Task OnGetAsync(int User_ID)
        {
            bookings = bookingCatalog.GetBookingsFromEvent(User_ID).Result;
        }
    }
}
