using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Coordinare.Services
{
    public class EventCatalog : Connection, IEventCatalog
    {
        private string GetAllSql = "SELECT * from Events";
        private string GetEventFromIDSql = "SELECT * from Events WHERE Event_ID = @ID";
        private string CreateEventSql = "INSERT into Events values(@Dur, @SID, @RID,  @Name, @Time, @Info, @SS)";
        private string DeleteEventSql = "DELETE from events where Event_ID = @ID";
        private string UpdateEventSql =
            "UPDATE Events set Duration = @Dur, Speaker = @SID,  Room_ID = @RID, EventName = @Name, DateTime = @Time, Eventinfo = @Info, SS_Amount = @SS WHERE Event_ID = @ID";

        private string GetBookingsSql = "SELECT * from Bookings WHERE Event_ID = @ID";

        private IUserCatalog _userCatalog;
        public EventCatalog(IConfiguration configuration, IUserCatalog _userCatalog) : base(configuration)
        {
        }

        public EventCatalog(string connectionString, IUserCatalog _userCatalog) : base(connectionString)
        {
        }

        public List<Event> Events { get; set; }

        public async Task<List<Event>> GetAllEvents()
        {
            List<Event> el = new List<Event>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {

                await using (SqlCommand command = new SqlCommand(GetAllSql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int eventId = reader.GetInt32(i: 0);
                            long dur = reader.GetInt64(i: 1);
                            TimeSpan duration = TimeSpan.FromTicks(dur);
                            int speaker = reader.GetInt32(i: 2);
                            string roomId = null;
                            if (!reader.IsDBNull(i: 3))
                            {
                                roomId = reader.GetString(i: 3);
                            }
                            string eventname = reader.GetString(i: 4);
                            DateTime datetime = reader.GetDateTime(i: 5);
                            string info = null;
                            if (!reader.IsDBNull(i: 6))
                            {
                                info = reader.GetString(i: 6);
                            }
                            int ss = reader.GetInt32(i: 7);

                            Event _event = new Event(eventId, duration, speaker, roomId, eventname, datetime, info, ss);
                            el.Add(_event);
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                        return null;
                    }
                }
            }
            return el;
        }

        public async Task<Event> GetEventFromId(int id)
        {
            Event _event = null;
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(GetEventFromIDSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int eventId = reader.GetInt32(i: 0);
                            long dur = reader.GetInt64(i: 1);
                            TimeSpan duration = TimeSpan.FromTicks(dur);
                            int speaker = reader.GetInt32(i: 2);
                            string roomId = null;
                            if (!reader.IsDBNull(i: 3))
                            {
                                roomId = reader.GetString(i: 3);
                            }
                            string eventname = reader.GetString(i: 4);
                            DateTime datetime = reader.GetDateTime(i: 5);
                            string info = null;
                            if (!reader.IsDBNull(i: 6))
                            {
                                info = reader.GetString(i: 6);
                            }
                            int ss = reader.GetInt32(i: 7);
                            _event = new Event(eventId, duration,speaker, roomId, eventname, datetime, info, ss);
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                        return null;
                    }
                }
            }
            return _event;
        }

        public async void CreateEvent(Event _event)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(CreateEventSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Dur", _event.Duration.Ticks);
                        command.Parameters.AddWithValue("@SID", _event.Speaker);
                        command.Parameters.AddWithValue("@RID", string.IsNullOrEmpty(_event.Room_ID) ? (object)DBNull.Value : _event.Room_ID);
                        command.Parameters.AddWithValue("@Info", string.IsNullOrEmpty(_event.Eventinfo) ? (object)DBNull.Value : _event.Eventinfo);
                        command.Parameters.AddWithValue("@Name", _event.EventName);
                        command.Parameters.AddWithValue("@Time", _event.DateTime);
                        command.Parameters.AddWithValue("@SS", _event.SS_amount);
                        await command.Connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
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
        }

        public async void DeleteEvent(int id)
        {
            //"DELETE from events where Event_ID = @ID";
            Event _event = await GetEventFromId(id);
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(DeleteEventSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        await command.Connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (SqlException)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Generel Fejl");
                    }
                }
            }
        }

        public async void UpdateEvent(Event _event, int id)
        {
            // "UPDATE Events set Duration = @Dur, Room_ID = @RID, EventName = @Name, DateTime = @Time, Eventinfo = @Info, SS_Amount = @SS";

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(UpdateEventSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", _event.Event_ID);
                        command.Parameters.AddWithValue("@Dur", _event.Duration.Ticks);
                        command.Parameters.AddWithValue("@SID", _event.Speaker);
                        command.Parameters.AddWithValue("@RID", string.IsNullOrEmpty(_event.Room_ID) ? (object)DBNull.Value : _event.Room_ID);
                        command.Parameters.AddWithValue("@Info", string.IsNullOrEmpty(_event.Eventinfo) ? (object)DBNull.Value : _event.Eventinfo);
                        command.Parameters.AddWithValue("@Name", _event.EventName);
                        command.Parameters.AddWithValue("@Time", _event.DateTime);
                        command.Parameters.AddWithValue("@SS", _event.SS_amount);
                        await command.Connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        await command.Connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (SqlException sqx)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                    }
                }
            }
        }

        public async Task<List<object>> GetWaitingList<T>()
        {
            List<object> bl = new List<object>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(GetBookingsSql, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int bookingid = reader.GetInt32(i: 0);
                        int eventid = reader.GetInt32(i: 1);
                        int userid = reader.GetInt32(i: 2);
                        bool Sseat = reader.GetBoolean(3);
                        bool inWaiting = reader.GetBoolean(i: 4);
                        DateTime date = reader.GetDateTime(i: 5);
                        var booking = new { };
                        bl.Add(booking);
                    }
                }
                catch (SqlException)
                {
                    Console.WriteLine("Database Fejl");
                    return null;
                }
                catch (Exception)
                {
                    Console.WriteLine("Generel Fejl");
                    return null;
                }
            }
            return bl;
        }

    }
}
