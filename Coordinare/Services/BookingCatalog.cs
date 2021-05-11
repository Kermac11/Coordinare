using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coordinare.Services
{
    public class BookingCatalog : Connection, IBookingCatalog
    {
        #region SQLStrings

        private String queryString = "select * from Bookings";
        private String UserqueryString = "select * from Bookings WHERE User_ID = @userID";
        private String EventqueryString = "select * from Bookings WHERE Event_ID = @eventID";
        private string QueryIDString = "select * from Bookings where Booking_ID = @ID";
        private String insertSql = "insert into Bookings Values (@eventID, @userID, @specialseat, @inwaitinglist, @bookingdate)";
        private String deleteSql = "delete from Bookings where Booking_ID = @bookingID AND Event_ID = @eventID AND User_ID = @userID";

        private String updateSql = "update Bookings " +
                                   "set Special_Seat = @specialseat, InWaitingList = @inwaitinglist, BookingDate = @bookingdate" +
                                   "where Booking_ID = @bookingID AND Event_ID = @eventID AND User_ID = @userID";

        #endregion SQLStrings

        #region constructors

        public BookingCatalog(IConfiguration configuration) : base(configuration)
        {
        }

        public BookingCatalog(string connectionString) : base(connectionString)
        {
        }

        #endregion constructors

        #region methods

        public async Task<List<Booking>> GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    await command.Connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int bookingid = reader.GetInt32(0);
                        int eventid = reader.GetInt32(1);
                        int userid = reader.GetInt32(2);
                        bool specialseat = reader.GetBoolean(3);
                        bool inwatiinglist = reader.GetBoolean(4);
                        DateTime bookingdate = reader.GetDateTime(5);
                        Booking booking = new Booking(bookingid, eventid, userid, specialseat, inwatiinglist, bookingdate);
                        bookings.Add(booking);
                    }
                }
                catch (SqlException sx)
                {
                    Console.WriteLine("Database Fejl");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel Fejl");
                }
            }
            return bookings;
        }

        public async Task<Booking> GetBookingFromid(int id)
        {
            Booking booking = new Booking();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(QueryIDString, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.Read())
                        {
                            int bookingid = reader.GetInt32(0);
                            int eventid = reader.GetInt32(1);
                            int userid = reader.GetInt32(2);
                            bool specialseat = reader.GetBoolean(3);
                            bool inwatiinglist = reader.GetBoolean(4);
                            DateTime bookingdate = reader.GetDateTime(5);
                            booking = new Booking(bookingid, eventid, userid, specialseat, inwatiinglist, bookingdate);
                            return booking;
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                    }

                    return null;
                }
            }
        }

        public async Task<List<Booking>> GetBookingsFromUser(int userID)
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(UserqueryString, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@userID", userID);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            int bookingid = reader.GetInt32(0);
                            int eventid = reader.GetInt32(1);
                            int userid = reader.GetInt32(2);
                            bool specialseat = reader.GetBoolean(3);
                            bool inwatiinglist = reader.GetBoolean(4);
                            DateTime bookingdate = reader.GetDateTime(5);
                            Booking booking = new Booking(bookingid, eventid, userid, specialseat, inwatiinglist, bookingdate);
                            bookings.Add(booking);
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                    }
                }
            }
            return bookings;
        }

        public async Task<bool> CreateBooking(Booking booking)
        {
            if (GetAllBookings().Result.FirstOrDefault(b => b.Event_ID == booking.Event_ID && b.User_ID == booking.User_ID) != null)
            {
                return false;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@eventID", booking.Event_ID);
                        command.Parameters.AddWithValue("@userID", booking.User_ID);
                        command.Parameters.AddWithValue("@specialseat", booking.Special_Seat);
                        command.Parameters.AddWithValue("@inwaitinglist", booking.InWaitingList);
                        command.Parameters.AddWithValue("@bookingdate", booking.BookingDate);
                        await command.Connection.OpenAsync();

                        int NoOfRowsAffected = await command.ExecuteNonQueryAsync(); //bruges ved update, delete, insert
                        if (NoOfRowsAffected == 1)
                        {
                            return true;
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                    }
                    return false;
                }
            }
        }

        public async Task<Booking> DeleteBooking(int id)
        {
            Booking targetBooking = new Booking();
            targetBooking = await GetBookingFromid(id);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@bookingID", targetBooking.Booking_ID);
                        command.Parameters.AddWithValue("@eventID", targetBooking.Event_ID);
                        command.Parameters.AddWithValue("@userID", targetBooking.User_ID);

                        await command.Connection.OpenAsync();

                        int NoOfRowsAffected = await command.ExecuteNonQueryAsync(); //bruges ved update, delete, insert
                        if (NoOfRowsAffected == 1)
                        {
                            return targetBooking;
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                    }
                }

                return null;
            }
        }

        public async Task<bool> UpdateBooking(Booking booking, int bookingid, int eventid, int userid)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@bookingID", bookingid);
                        command.Parameters.AddWithValue("@eventID", eventid);
                        command.Parameters.AddWithValue("@userID", userid);
                        command.Parameters.AddWithValue("@specialseat", booking.Special_Seat);
                        command.Parameters.AddWithValue("@inwaitinglist", booking.InWaitingList);
                        command.Parameters.AddWithValue("@bookingdate", booking.BookingDate);
                        await command.Connection.OpenAsync();
                        int NoOfRowsAffected = await command.ExecuteNonQueryAsync();

                        if (NoOfRowsAffected == 1)
                        {
                            return true;
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                    }
                }

                return false;
            }
        }

        public async Task<List<Booking>> GetBookingsFromEvent(int eventID)
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(EventqueryString, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@eventID", eventID);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            int bookingid = reader.GetInt32(0);
                            int eventid = reader.GetInt32(1);
                            int userid = reader.GetInt32(2);
                            bool specialseat = reader.GetBoolean(3);
                            bool inwatiinglist = reader.GetBoolean(4);
                            DateTime bookingdate = reader.GetDateTime(5);
                            Booking booking = new Booking(bookingid, eventid, userid, specialseat, inwatiinglist, bookingdate);
                            bookings.Add(booking);
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                    }
                }
            }
            return bookings;
        }

        #endregion methods
    }
}