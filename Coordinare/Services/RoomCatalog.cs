using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Coordinare.Exceptions;

namespace Coordinare.Services
{
    #region MySql

    public class RoomCatalog : Connection, IRoomCatalog
    {
        private string queryString = "Select * from Rooms";
        private string queryStringFromID = "select * from Rooms where Room_ID =@ID";
        private string insertSql = "insert into Rooms Values(@ID, @Capacity)";
        private string deleteSql = "delete from Rooms where Room_ID = @ID";
        private string updateSql = "update Rooms set Room_ID=@ID, Capacity=@Capacity where Room_ID = @RID";

        #endregion

        #region Sql Connection

        public RoomCatalog(IConfiguration configuration) : base(configuration)
        {
        }


        public RoomCatalog(string connectionString) : base(connectionString)
        {
        }


        public List<Room> Rooms { get; set; }

        #endregion


        #region CreateRoom

        public async Task<bool> CreateRoomAsync(Room room)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using SqlCommand command = new SqlCommand(insertSql, connection);
                {
                    try
                    {
                        if (IdExist(room.Room_ID))
                        {
                            throw new ExistsException("Room ID already exists, please choose another ID.");
                        }

                        command.Parameters.AddWithValue("@ID", room.Room_ID);
                        command.Parameters.AddWithValue("@Capacity", room.Capacity);
                        await command.Connection.OpenAsync();

                        int noOfRows = command.ExecuteNonQuery(); //bruges ved update, delete, insert
                        if (noOfRows == 1)
                        {
                            return true;
                        }
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
                return false;
            }

        }

        #endregion


        #region DeleteRoom

        public async Task<Room> DeleteRoomAsync(string id)
        {
            Room room = await GetRoomsFromIdAsync(id);
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        command.Parameters.AddWithValue("@ID", id);

                        int noOfRows = command.ExecuteNonQuery(); //bruges ved update, delete, insert
                        if (noOfRows == 1)
                        {
                            return room;
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

            return null;
        }

        #endregion


        #region GetAllRooms

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            List<Room> Rooms = new List<Room>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            String Room_ID = reader.GetString(0);
                            int Capacity = reader.GetInt32(1);
                            Room room = new Room(Room_ID, Capacity);
                            Rooms.Add(room);
                        }
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

            return Rooms;
        }

        #endregion


        #region GetRoomsFromId

        public async Task<Room> GetRoomsFromIdAsync(string id)
        {
            Room room = null;
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryStringFromID, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            string roomId = reader.GetString(0);
                            int capacity = reader.GetInt32(1);
                            room = new Room(roomId, capacity);
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

            return room;
        }

        #endregion


        #region Update

        public async Task<bool> UpdateRoomAsync(Room Room, string id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", Room.Room_ID);
                        command.Parameters.AddWithValue("@Capacity", Room.Capacity);
                        command.Parameters.AddWithValue("@RID", id);

                        if (IdExist(Room.Room_ID))
                        {
                            throw new ExistsException("Room ID already exists, please choose another ID.");
                        }

                        await command.Connection.OpenAsync();
                        // repeat for all variables....
                        int noOfRows = await command.ExecuteNonQueryAsync(); //bruges ved update, delete, insert
                        if (noOfRows == 1)
                        {
                            
                            return true;
                        }
                    }
                    catch (SqlException sqlexception)
                    {
                        Console.WriteLine("Database Fejl");
                        return false;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Generel Fejl");
                        return false;
                    }
                }
            }
            return false;
        }

        #endregion


        #region IdExist

        private bool IdExist(string id)
        {
            foreach (Room r_id in GetAllRoomsAsync().Result)
            {
                if (r_id.Room_ID == id)
                    return true;
            }

            return false;
        }

        #endregion
    }
}
