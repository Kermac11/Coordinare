﻿using Coordinare.Models;
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

        Task<bool> CreateRoomAsync(Room room);

        void DeleteRoomAsync(string id);

        Task<bool> UpdateRoomAsync(Room room, string id);
    }
}
