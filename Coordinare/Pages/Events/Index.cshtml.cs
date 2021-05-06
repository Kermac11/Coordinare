using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Coordinare.Models;
using Coordinare.Pages.Users;

namespace Coordinare.Pages.Events
{
    public class IndexModel : PageModel
    {
        private IEventCatalog _service;
        private IUserCatalog _usercatalog;
        public List<Event> Events { get; set; }
        public IndexModel(IEventCatalog service, IUserCatalog uservice)
        {
            _service = service;
            _usercatalog = uservice;
        }
        public void OnGet()
        {
            Events = _service.GetAllEvents().Result;

        }

        public async Task<IActionResult> OnPostSearch(string filter)
        {

            if (!string.IsNullOrEmpty(filter))
            {
                Events = _service.SearchByFilter(filter).Result;
            }
            Events ??= _service.GetAllEvents().Result;
           
            return Page();
        }
    }
}
