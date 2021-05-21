using Coordinare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coordinare.Interfaces
{
    public interface IRoomCatalog
    {

        Task<List<Room>> GetAllRoomsAsync();

        Task<Room> GetRoomsFromIdAsync(string id);

        Task<bool> CreateRoomAsync(Room room);

        Task<Room> DeleteRoomAsync(string id);

        Task<bool> UpdateRoomAsync(Room room, string id);
    }
}
