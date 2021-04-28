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
        // private string CreateEventSql = "INSERT into Events values(@ID, @Dur, @RID, @Name, @Time, @Info, @SS)";
        private string DeleteEventSql = "DELETE from events where Event_ID = @ID";
        private string UpdateEventSql =
            "UPDATE Events set Duration = @Dur, Room_ID = @RID, EventName = @Name, DateTime = @Time, Eventinfo = @Info, SS_Amount = @SS";

        private string GetBookingsSql = "SELECT * from Bookings WHERE Event_ID = @ID";
        public EventCatalog(IConfiguration configuration) : base(configuration)
        {
        }

        public EventCatalog(string connectionString) : base(connectionString)
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
                            string duration = reader.GetString(i: 1);
                            string roomId = null;
                            if (!reader.IsDBNull(i: 2))
                            {
                                roomId = reader.GetString(i: 2);
                            }
                            string eventname = reader.GetString(i: 3);
                            DateTime datetime = reader.GetDateTime(i: 4);
                            string info = reader.GetString(i: 5);
                            int ss = reader.GetInt32(i: 6);
                            Event _event = new Event(eventId, duration, roomId, eventname, datetime, info, ss);
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
                            string duration = reader.GetString(i: 1);
                            string roomId = reader.GetString(i: 2);
                            string eventname = reader.GetString(i: 3);
                            DateTime datetime = reader.GetDateTime(i: 4);
                            string info = reader.GetString(i: 5);
                            int ss = reader.GetInt32(i: 6);
                            _event = new Event(eventId, duration, roomId, eventname, datetime, info, ss);
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
            }
            return _event;
        }

        public async void CreateEvent(Event _event)
        {
            List<PropertyInfo> prop = _event.GetType().GetProperties().ToList();
            string createsql = "INSERT into Events values(";
            for (int i = 0; i < prop.Count; i++)
            {
                createsql += $"@para{1}";
            }

            createsql += ")";
            //private string CreateEventSql = "INSERT into Events values(@ID, @Dur, @RID, @Name, @Time, @Info, @SS)";
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(createsql, connection))
                {
                    try
                    {
                        for (int i = 0; i < prop.Count; i++)
                        {
                            command.Parameters.AddWithValue($"@para{1}", prop[1].GetValue(prop[i]));
                        }
                        await command.Connection.OpenAsync();
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
            List<PropertyInfo> prop = _event.GetType().GetProperties().ToList();
            string updatesql = "UpDATE Events set";
            for (int i = 0; i < prop.Count; i++)
            {
                updatesql += $"{prop[i].Name} @para{1}, ";
            }

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(updatesql, connection))
                {
                    try
                    {
                        for (int i = 0; i < prop.Count; i++)
                        {
                            command.Parameters.AddWithValue($"@para{1}", prop[1].GetValue(prop[i]));
                        }
                        await command.Connection.OpenAsync();
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
