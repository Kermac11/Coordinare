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
        public List<Booking> Bookings { get; set; }
        public List<Event> BookedEvents { get; set; }
        public List<int> NumOfDates { get; set; }

        public ScheduleModel(IEventCatalog eventCatalog, IBookingCatalog bookingCatalog)
        {
            this.eventCatalog = eventCatalog;
            this.bookingCatalog = bookingCatalog;
        }

        public async Task OnGetAsync(int id)
        {
            Bookings = await bookingCatalog.GetBookingsFromUser(id);
            BookedEvents = await GetBookedEvents();
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
        //    return Events.Select(e => e.DateTime.Day).Distinct().Count();
        //}
    }
}
