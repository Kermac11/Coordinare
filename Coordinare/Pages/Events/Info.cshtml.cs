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
        private ITagSelection _tservice;
        public LoginService _Lservice;
        private IRoomCatalog _Rservice;
        public List<Booking> UserBookings;
        public Event Event { get; set; }
        public User CurrentUser { get; set; }
        public List<Tag> Tags { get; set; }
        public int Seats { get; set; }
        public Room Room { get; set; }
        public List<Booking> Bookings { get; set; }

        [BindProperty] public int EventID { get; set; }

        public InfoModel(IEventCatalog service, IBookingCatalog Bservice, LoginService Lservice, ITagSelection tservice, IRoomCatalog RService)
        {
            _service = service;
            _Bservice = Bservice;
            _Lservice = Lservice;
            _tservice = tservice;
            _Rservice = RService;
        }
        public void OnGet(int id)
        {
          Event = _service.GetEventFromId(id).Result;
          EventID = id;
          CurrentUser = _Lservice.GetLoggedInUser();
          if (CurrentUser != null)
          { 
              UserBookings = _Bservice.GetBookingsFromUser(CurrentUser.User_ID).Result;
          }
          Tags = _tservice.GetAllTags().Result.FindAll(t => t.Event_ID == id);
          Seats = _Bservice.GetBookingsFromEvent(id).Result.Count;
          Room = _Rservice.GetRoomsFromIdAsync(Event.Room_ID).Result;
          Bookings = _Bservice.GetBookingsFromEvent(id).Result;
          Tags ??= new List<Tag>();
            //Tags ??= new List<Tag>() er det samme som if statement nedenunder.
            //if (Tags == null)
            //{
            //    Tags = new List<Tag>();
            //}
            Room ??= new Room();

        }

        public async Task<IActionResult> OnPost()
        {
            return RedirectToPage("/Bookings/CreateBooking", new { EventID = this.EventID });
        }

    }
}
