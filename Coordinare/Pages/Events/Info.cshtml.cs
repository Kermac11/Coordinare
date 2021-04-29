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
    public class InfoModel : PageModel
    {
        private IEventCatalog _service;
        public Event Event { get; set; }
        public InfoModel(IEventCatalog service)
        {
            _service = service;
        }
        public  void OnGet(int id)
        {
          Event = _service.GetEventFromId(id).Result;
        }
    }
}
