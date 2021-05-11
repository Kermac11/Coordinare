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
        [BindProperty]
        public string Filter { get; set; }
        [BindProperty]
        public string DateSort { get; set; }
        [BindProperty]
        public string NameSort { get; set; }
        [BindProperty]
        public string SeatSort { get; set; }
        public IndexModel(IEventCatalog service, IUserCatalog uservice)
        {
            _service = service;
            _usercatalog = uservice;
        }
        public void OnGet()
        {
            Events = _service.GetAllEvents().Result;
            Events = Events.FindAll(e => e.DateTime > DateTime.UtcNow);
            DateSort ??= DateSort = "down";
            NameSort ??= NameSort = "down";
            SeatSort ??= SeatSort = "down";
        }

        public async Task<IActionResult> OnPostSearch(string filter, string option)
        {
            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(option))
            {
                if (option == "name")
                {
                    Filter = filter;
                    Events = _service.GetAllEvents().Result.FindAll(e => e.DateTime > DateTime.UtcNow).FindAll(e => e.EventName.ToLower().Contains(filter.ToLower()));
                    Events.Sort((e1, e2) => e1.EventName.CompareTo(e2.EventName));
                }
            }
            if (Events == null)
            {
                Events = _service.GetAllEvents().Result;
                Events = Events.FindAll(e => e.DateTime > DateTime.UtcNow);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSortdate(string sort)
        {
            if (sort == "down")
            {
                if (!string.IsNullOrEmpty(Filter))
                {
                    Events = _service.GetAllEvents().Result.FindAll(e => e.DateTime > DateTime.UtcNow).FindAll(e => e.EventName.ToLower().Contains(Filter.ToLower()));
                }
                else
                {
                    Events = _service.GetAllEvents().Result;
                    Events = Events.FindAll(e => e.DateTime > DateTime.UtcNow);
                }
                Events.Sort((e1, e2) => e2.DateTime.CompareTo(e1.DateTime));
                DateSort = "up";
                NameSort = "down";
                SeatSort = "down";
            }
            else
            {
                if (!string.IsNullOrEmpty(Filter))
                {
                    Events = _service.GetAllEvents().Result.FindAll(e => e.DateTime > DateTime.UtcNow).FindAll(e => e.EventName.ToLower().Contains(Filter.ToLower()));
                }
                else
                {
                    Events = _service.GetAllEvents().Result;
                    Events = Events.FindAll(e => e.DateTime > DateTime.UtcNow);
                }
                Events.Sort((e1, e2) => e1.DateTime.CompareTo(e2.DateTime));
                DateSort = "down";
                NameSort = "down";
                SeatSort = "down";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSortname(string sort)
        {
            if (sort == "down")
            {
                if (!string.IsNullOrEmpty(Filter))
                {
                    Events = _service.GetAllEvents().Result.FindAll(e => e.DateTime > DateTime.UtcNow).FindAll(e => e.EventName.ToLower().Contains(Filter.ToLower()));
                }
                else
                {
                    Events = _service.GetAllEvents().Result;
                    Events = Events.FindAll(e => e.DateTime > DateTime.UtcNow);
                }
                Events.Sort((e1, e2) => e2.EventName.CompareTo(e1.EventName));
                NameSort = "up";
                DateSort = "down";
                SeatSort = "down";
            }
            else
            {
                if (!string.IsNullOrEmpty(Filter))
                {
                    Events = _service.GetAllEvents().Result.FindAll(e => e.DateTime > DateTime.UtcNow).FindAll(e => e.EventName.ToLower().Contains(Filter.ToLower()));
                }
                else
                {
                    Events = _service.GetAllEvents().Result;
                    Events = Events.FindAll(e => e.DateTime > DateTime.UtcNow);
                }
                Events.Sort((e1, e2) => e1.EventName.CompareTo(e2.EventName));
                NameSort = "down";
                DateSort = "down";
                SeatSort = "down";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSortseat(string sort)
        {
            if (sort == "down")
            {
                if (!string.IsNullOrEmpty(Filter))
                {
                    Events = _service.GetAllEvents().Result.FindAll(e => e.DateTime > DateTime.UtcNow).FindAll(e => e.EventName.ToLower().Contains(Filter.ToLower()));
                }
                else
                {
                    Events = _service.GetAllEvents().Result;
                    Events = Events.FindAll(e => e.DateTime > DateTime.UtcNow);
                }
                Events.Sort((e1, e2) => e2.SS_amount.CompareTo(e1.SS_amount));
                NameSort = "down";
                DateSort = "down";
                SeatSort = "up";
            }
            else
            {
                if (!string.IsNullOrEmpty(Filter))
                {
                    Events = _service.GetAllEvents().Result.FindAll(e => e.DateTime > DateTime.UtcNow).FindAll(e => e.EventName.ToLower().Contains(Filter.ToLower()));
                }
                else
                {
                    Events = _service.GetAllEvents().Result;
                    Events = Events.FindAll(e => e.DateTime > DateTime.UtcNow);
                }
                Events.Sort((e1, e2) => e1.SS_amount.CompareTo(e2.SS_amount));
                NameSort = "down";
                DateSort = "down";
                SeatSort = "down";
            }

            return Page();
        }
    }
}
