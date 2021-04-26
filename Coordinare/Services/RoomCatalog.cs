using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coordinare.Services
{
    public class RoomCatalog : Connection, IRoomCatalog
    {
        private string queryString = "Select * from Rooms";
        private string queryStringFromID = "select * from Rooms where Room_ID =@ID";
        private string insertSql = "insert into Rooms Values(@ID, @Capacity)";
        private string deleteSql = "delete from Rooms where Room_ID = @ID";
        private string updateSql = "update Rooms set Room_ID=@ID, Capacity=@Capacity";


        public RoomCatalog(IConfiguration configuration) : base(configuration) { }


        public RoomCatalog(string connectionString) : base(connectionString) { }


        public List<Room> Rooms { get; set; }

        public void CreateRoomAsync(Room room)
        {
            throw new NotImplementedException();
        }

        public void DeleteRoomAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> GetAllRoomsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Room> GetRoomsFromIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoomAsync(Room room, string id)
        {
            throw new NotImplementedException();
        }
    }
}
