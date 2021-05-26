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
            BookedEvents = await bookingCatalog.GetBookedEvents((int)id);
            if (BookedEvents.Count != 0)
            {
                BookedEvents.Sort((e1, e2) => e1.DateTime.CompareTo(e2.DateTime));
                NumOfDates = BookedEvents.Select(e => e.DateTime).Distinct().ToList();
            }
            
            if (User == null)
                return NotFound();

            return Page();
        }

        // flyttet over i booking catalog
        ////
        //public async Task<List<Event>> GetBookedEvents()
        //{
        //    Event ev = new Event();
        //    List<Event> events = new List<Event>();
        //    foreach (Booking b in Bookings)
        //    {
        //        ev = await eventCatalog.GetEventFromId(b.Event_ID);
        //        events.Add(ev);
        //    }
        //    return events;
        //}

        public Dictionary<DateTime, string> GetDateDict()
        {
            Dictionary<DateTime,string> dates = new Dictionary<DateTime, string>();
            int a = 1;

            foreach (var d in BookedEvents.Select(e => e.DateTime.Date).Distinct().ToList())
            {
                string value = "Day " + a;
                dates.Add(d, value);
                a++;
            }
            
            return dates;
        }
        public async Task<string> CalculateTime(DateTime from, DateTime now)
        {
            string timestring = null;
            TimeSpan cal = from - now;
            timestring += cal.Days > 0 ? $"Days: {cal.Days} " : "";
            timestring += $"Hours: {cal.Hours} " + $"Minutes: {cal.Minutes}";
            return timestring;
        }
        
    }
}
