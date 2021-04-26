using Coordinare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coordinare.Interfaces
{
    public interface IRoomCatalog
    {
        List<Room> Rooms { get; set; }

        Task<List<Room>> GetAllRoomsAsync();

        Task<Room> GetRoomsFromIdAsync(string id);

        void CreateRoomAsync(Room room);

        void DeleteRoomAsync(string id);

        void UpdateRoomAsync(Room room, string id);
    }
}
