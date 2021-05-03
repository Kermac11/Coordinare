using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coordinare.Models
{
    public class Booking
    {
        public Booking(int bookingId, int eventId, int userId, bool specialSeat, bool inWaitingList, DateTime bookingDate )
        {
            Booking_ID = bookingId;
            Event_ID = eventId;
            User_ID = userId;
            Special_Seat = specialSeat;
            InWaitingList = inWaitingList;
            BookingDate = bookingDate;
        }

        public Booking()
        {
            
        }
        public int Booking_ID { get; set; }

        public int Event_ID { get; set; }

        public int User_ID { get; set; }

        public bool Special_Seat { get; set; }

        public bool InWaitingList { get; set; }

        public DateTime BookingDate { get; set; }

    }
}
