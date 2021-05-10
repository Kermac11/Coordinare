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

        public Event(int eventId, TimeSpan duration, User speaker, string roomId, string eventName, DateTime dateTime, string eventinfo, int ssAmount)
        {
            Event_ID = eventId;
            Duration = duration;
            Speaker = speaker;
            Room_ID = roomId;
            EventName = eventName;
            DateTime = dateTime;
            Eventinfo = eventinfo;
            SS_amount = ssAmount;
        }

        public int Event_ID { get; set; }
        [Required(ErrorMessage = "Duration is required")]
        public TimeSpan Duration  { get; set; }
        [Required(ErrorMessage = "Speaker Needed")]
        public User Speaker { get; set; }
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
