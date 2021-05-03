using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Pages;

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
        [Required(ErrorMessage = "Duration is required")]
        public string Duration  { get; set; }
        public string Room_ID { get; set; }
        [Required (ErrorMessage = "Name Needed")]
        public string EventName { get; set; }
        [Required (ErrorMessage = "Date is needed")]
        public DateTime DateTime { get; set; }
        public string Eventinfo { get; set; }
        [Required (ErrorMessage = "SS amount needed")]
        public int SS_amount { get; set; }

    }
}
