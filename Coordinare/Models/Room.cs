using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coordinare.Models
{
    public class Room
    {
        public Room()
        {

        }
        public Room(string roomId, int capacity)
        {
            Room_ID = roomId;
            Capacity = capacity;
        }

        public string Room_ID { get; set; }

        public int Capacity { get; set; }

        
        
    }
}
