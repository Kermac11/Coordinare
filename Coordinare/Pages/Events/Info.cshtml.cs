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
        private LoginService _Lservice;

        public Event Event { get; set; }
        public User User { get; set; }
        public User CurrentUser { get; set; }

        [BindProperty] public int EventID { get; set; }

        public InfoModel(IEventCatalog service, LoginService Lservice)
        {
            _service = service;
            _Lservice = Lservice;
        }
        public void OnGet(int id)
        {
          Event = _service.GetEventFromId(id).Result;
          EventID = id;
          CurrentUser = _Lservice.GetLoggedInUser();
        }

        public async Task<IActionResult> OnPost()
        {
            return RedirectToPage("Bookings/CreateBooking"/*, new { EventID = this.EventID }*/);
        }
    }
}
