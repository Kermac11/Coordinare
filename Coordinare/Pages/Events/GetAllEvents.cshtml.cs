using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Coordinare.Interfaces;
using Coordinare.Models;

namespace Coordinare.Pages.Events
{
    public class GetAllEventsModel : PageModel
    {
        private IEventCatalog _service;
        public List<Event> Events { get; set; }
        public GetAllEventsModel(IEventCatalog service)
        {
            _service = service;
        }
        public async void OnGetAsync()
        {
            Events = _service.GetAllEvents().Result;
        }
    }
}
