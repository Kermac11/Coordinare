using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Interfaces;
using Coordinare.Models;
using Coordinare.Services;

namespace Coordinare.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public LoginService logService;
        private IEventCatalog eventCatalog;
        private IBookingCatalog bookingCatalog;
        private IUserCatalog userCatalog;

        public List<Booking> Bookings { get; set; }
        public List<Event> BookedEvents { get; set; }
        [BindProperty] public new User User { get; set; }
        public new ArrayList DatesArrayList { get; set; }

        public IndexModel(ILogger<IndexModel> logger, LoginService logService, IEventCatalog eventCatalog, IBookingCatalog bookingCatalog, IUserCatalog userCatalog)
        {
            _logger = logger;
            this.logService = logService;
            this.eventCatalog = eventCatalog;
            this.bookingCatalog = bookingCatalog;
            this.userCatalog = userCatalog;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (logService.GetLoggedInUser() != null)
            {
                User = await userCatalog.GetUserFromIdAsync(logService.GetLoggedInUser().User_ID);
                Bookings = await bookingCatalog.GetBookingsFromUser(logService.GetLoggedInUser().User_ID);
                BookedEvents = await bookingCatalog.GetBookedEvents(logService.GetLoggedInUser().User_ID);
                BookedEvents.Sort((e1, e2) => e1.DateTime.CompareTo(e2.DateTime));

                DatesArrayList = Dates().Result;

                if (User == null)
                    return NotFound();
            }

            return Page();
        }

        public async Task<ArrayList> Dates()
        {
            ArrayList list = new ArrayList();
            foreach (var d in BookedEvents)
            {
                list.Add(d.DateTime);
            }

            return list;
        }
    }
}
