using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Coordinare.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Coordinare.Models;
using Coordinare.Pages.Users;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.WebEncoders.Testing;

namespace Coordinare.Pages.Events
{
    public class IndexModel : PageModel
    {
        private IEventCatalog _service;
        private IUserCatalog _usercatalog;
        public List<Event> Events { get; set; }
        public List<SelectListItem> Filter { get; set; }
        public string DateSort { get; set; }
        public IndexModel(IEventCatalog service, IUserCatalog uservice)
        {
            _service = service;
            _usercatalog = uservice;
            DateSort = "down";
        }
        public void OnGet()
        {
            Events = _service.GetAllEvents().Result;
            Events = Events.FindAll(e => e.DateTime > DateTime.UtcNow);
        }

        public async Task<IActionResult> OnPostSearch(string filter, string option)
        {
            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(option))
            {
                if (option == "name")
                {
                    Events = _service.GetAllEvents().Result.FindAll(e => e.EventName.ToLower().Contains(filter.ToLower()));
                    Events.Sort((e1, e2) => e1.EventName.CompareTo(e2.EventName));
                }
            }
            if (Events == null)
            {
                OnGet();
            }

            //Events.Select(e => e.DateTime.Day).Distinct().Count();

            return Page();
        }

        public async Task<IActionResult> OnPostSortdate()
        {

            if (DateSort == "down")
            {
                DateSort = "up";
                OnGet();
                Events.Sort((e1, e2) => e2.EventName.CompareTo(e1.EventName));
            }

            if (DateSort == "up")
            {
                DateSort = "down";
                OnGet();
                Events.Sort((e1, e2) => e1.EventName.CompareTo(e2.EventName));
            }
            return Page();
        }
    }
}
