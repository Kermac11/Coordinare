using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Coordinare.Pages.Events
{
    public class DeleteModel : PageModel
    {
        private IEventCatalog _service;
        private IUserCatalog _uservice;
        public Event Event { get; set; }
        public User Speaker { get; set; }
        public DeleteModel(IEventCatalog service, IUserCatalog uservice)
        {
            _service = service;
            _uservice = uservice;
        }
        public void OnGet(int id)
        {
          //  Func<int, bool> testbool = i => i == 0 ? true : false;
          Event = _service.GetEventFromId(id).Result;
          Speaker = _uservice.GetUserFromIdAsync(Event.Speaker).Result;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            _service.DeleteEvent(id);
            return RedirectToPage("GetAllEvents");
        }
    }
}
