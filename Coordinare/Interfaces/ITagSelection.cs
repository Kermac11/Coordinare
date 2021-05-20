using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Models;

namespace Coordinare.Interfaces
{
    public interface ITagSelection
    {
        public Task<List<string>> GetTags();
        public Task<List<Tag>> GetAllTags();
        public void AddNewTag(string tag);
        public void RemoveTag(string tag);
        public void CreateNewTagToEvent(Event e, string tag);
        public void DeleteTag(Event e, string tag);
    }
}
