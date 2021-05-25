using Coordinare.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coordinare.Interfaces
{
    public interface IBookingCatalog
    {

        Task<List<Booking>> GetAllBookings();

        Task<Booking> GetBookingFromid(int id);

        Task<List<Booking>> GetBookingsFromUser(int userID);

        Task<bool> CreateBooking(Booking booking);

        Task<Booking> DeleteBooking(int id);

        Task<bool> UpdateBooking(Booking booking, int bookingid, int eventid, int userid);

        Task<List<Booking>> GetBookingsFromEvent(int eventID);

        Task<List<Event>> GetBookedEvents(int id);
    }
}
