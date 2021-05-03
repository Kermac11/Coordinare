using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coordinare.Models
{
    public class Event
    {
        public Event()
        {
            
        }
        public Event(int eventId, string duration, string roomId, string eventName, DateTime dateTime, string eventinfo, int ssAmount)
        {
            Event_ID = eventId;
            Duration = duration;
            Room_ID = roomId;
            EventName = eventName;
            DateTime = dateTime;
            Eventinfo = eventinfo;
            SS_amount = ssAmount;
        }

        public Event(int eventId, string duration, string eventName, DateTime dateTime, string eventinfo, int ssAmount)
        {
            Event_ID = eventId;
            Duration = duration;
            EventName = eventName;
            DateTime = dateTime;
            Eventinfo = eventinfo;
            SS_amount = ssAmount;
        }
        
        public int Event_ID { get; set; }
        [Required]
        public string Duration  { get; set; }
        public string Room_ID { get; set; }
        [Required]
        public string EventName { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public string Eventinfo { get; set; }
        [Required]
        public int SS_amount { get; set; }

    }
}
