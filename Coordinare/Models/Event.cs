using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coordinare.Models
{
    public class Event
    {
        public int Event_ID { get; set; }
        public string Duration  { get; set; }
        public int Room_ID { get; set; }
        public string EventName { get; set; }
        public DateTime DateTime { get; set; }
        public string Eventinfo { get; set; }
        public int SS_amount { get; set; }

    }
}
