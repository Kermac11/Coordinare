using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coordinare.Pages.Events
{
    public class CreateModel : PageModel
    {
        private IEventCatalog _eservice;
        private IRoomCatalog _rservice;
        private IUserCatalog _uservice;
        private ITagSelection _tservice;

        private Object _lock = new object();
        [BindProperty] public Event Event { get; set; }
        public string Time { get; set; }
        public List<TagName> Tags { get; set; }
        public List<Room> Rooms { get; set; }
        public List<User> Speakers { get; set; }

        [BindProperty]
        public TimeSpan dura { get; set; }
        public CreateModel(IEventCatalog eservice, IRoomCatalog rservice, IUserCatalog uservice, ITagSelection tservice)
        {
            _eservice = eservice;
            _rservice = rservice;
            _uservice = uservice;
            _tservice = tservice;
            Rooms = _rservice.GetAllRoomsAsync().Result;
            Speakers = uservice.GetAllUsersAsync().Result.FindAll(u => u.Speaker == true);
        }

        public void OnGet()
        {
            Tags = _tservice.GetTagNames().Result;
            Tags ??= new List<TagName>();
            Time = DateTime.Now.ToString("yyyy-MM-ddT00:00");
        }

        public async Task<IActionResult> OnPostAsync(int sid, List<int> tag)
        {

            Event place = new Event();
            Event.LastUpdated = DateTime.Now;
            Event.Speaker = _uservice.GetUserFromIdAsync(sid).Result;
            if (!ModelState.IsValid && Event.Speaker == null)
            {
                return Page();
            }

            if (_eservice.GetAllEvents().Result.Exists(e => !(e.Room_ID == Event.Room_ID && ((Event.DateTime.Add(Event.Duration) < e.DateTime) || (Event.DateTime > e.DateTime.Add(e.Duration))))))
            {
                lock (_lock)
                {
                    _eservice.CreateEvent(Event);
                    List<Event> el = _eservice.GetAllEvents().Result;
                    place = el.Last();
                }
                foreach (int item in tag)
                {
                    _tservice.CreateNewTagToEvent(place, _tservice.GetTagNameFromId(item).Result);
                }
            }
            return RedirectToPage("GetAllEvents");

        }

    }
}
