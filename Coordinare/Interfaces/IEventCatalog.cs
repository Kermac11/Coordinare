using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Models;

namespace Coordinare.Interfaces
{
    public interface IEventCatalog
    {
        Task<List<Event>> GetAllEvents();
        Task<Event> GetEventFromId(int id);
        void CreateEvent(Event _event);
        void DeleteEvent(int id);
        void UpdateEvent(Event _event, int id);
        Task<List<object>> GetWaitingList<T>();
        Task<List<Event>> SearchByFilter(string filter);
    }
}
