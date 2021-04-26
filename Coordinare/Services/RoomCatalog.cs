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
        private string queryString = "";
        private string queryNameString = "";
        private string queryStringFromID = "";
        private string insertSql = "";
        private string deleteSql = "";
        private string updateSql = "";


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
