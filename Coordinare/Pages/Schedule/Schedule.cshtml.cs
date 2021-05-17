using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.AspNetCore.Components;
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
        public Predicate<Event> test;
        public List<Event> BookedEvents { get; set; }
        public List<DateTime> NumOfDates { get; set; }
        [BindProperty] public new User User { get; set; }
        public List<Event> EventList { get; set; }

        public ScheduleModel(IEventCatalog eventCatalog, IBookingCatalog bookingCatalog, IUserCatalog userCatalog)
        {
            this.eventCatalog = eventCatalog;
            this.bookingCatalog = bookingCatalog;
            this.userCatalog = userCatalog;
        }

        public async Task<IActionResult> OnGetAsync()
        {


            User = await userCatalog.GetUserFromIdAsync(1);
            Bookings = await bookingCatalog.GetBookingsFromUser(User.User_ID);
            BookedEvents = await GetBookedEvents();
            NumOfDates = BookedEvents.Select(e => e.DateTime).ToList();
            var dates = NumOfDates.Select(d => new { Day = d.Day, Month = d.Month }).Distinct().ToList();
            EventList = BookedEvents.FindAll(e => e.DateTime.Day == dates[0].Day && e.DateTime.Month == dates[0].Month);
            EventList ??= new List<Event>();
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

        public async Task<string> CalculateTime(DateTime from, DateTime now)
        {
            string timestring = null;
            TimeSpan cal = from - now;
            timestring += cal.Days > 0 ? $"Days:{cal.Days} " : "";
            timestring += $"Hours:{cal.Hours} " + $"Minutes:{cal.Minutes}";
            return timestring;
        }

        public async Task<IActionResult> OnPostDaylist(int day, int month)
        {
            EventList ??= new List<Event>();
            User = await userCatalog.GetUserFromIdAsync(User.User_ID);
            Bookings = bookingCatalog.GetBookingsFromUser(User.User_ID).Result;
            BookedEvents = await GetBookedEvents();
            EventList = BookedEvents.FindAll(e => e.DateTime.Day == day && e.DateTime.Month == month);
            NumOfDates = BookedEvents.Select(e => e.DateTime).ToList();

            return Page();
        }
    }
}
