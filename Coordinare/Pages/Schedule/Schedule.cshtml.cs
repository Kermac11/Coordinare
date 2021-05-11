using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coordinare.Pages.Schedule
{
    public class ScheduleModel : PageModel
    {
        private IEventCatalog eventCatalog;
        private IBookingCatalog bookingCatalog;
        private IUserCatalog userCatalog;
        public List<Booking> Bookings { get; set; }
        public List<Event> BookedEvents { get; set; }
        public List<DateTime> NumOfDates { get; set; }
        [BindProperty] public new User User { get; set; }

        public ScheduleModel(IEventCatalog eventCatalog, IBookingCatalog bookingCatalog, IUserCatalog userCatalog)
        {
            this.eventCatalog = eventCatalog;
            this.bookingCatalog = bookingCatalog;
            this.userCatalog = userCatalog;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            User = await userCatalog.GetUserFromIdAsync((int)id);
            Bookings = await bookingCatalog.GetBookingsFromUser((int)id);
            BookedEvents = await GetBookedEvents();
            NumOfDates = BookedEvents.Select(e => e.DateTime).Distinct().ToList();

            if (User == null)
                return NotFound();

            return Page();
        }

        public async Task<List<Event>> GetBookedEvents()
        {
            Event ev = new Event();
            List<Event> events = new List<Event>();
            foreach (Booking b in Bookings)
            {
                ev = await eventCatalog.GetEventFromId(b.Event_ID);
                events.Add(ev);
            }
            return events;
        }

        //public async Task<List<int>> GetIntDates()
        //{
        //    List<int> ints = new List<int> {BookedEvents.Select(e => e.DateTime.Day).Distinct().Count()};
        //    return ints;
        //}
    }
}
