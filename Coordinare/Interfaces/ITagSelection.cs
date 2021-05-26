using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Models;

namespace Coordinare.Interfaces
{
    public interface ITagSelection
    {
        public Task<List<TagName>> GetTagNames();
        public Task<List<Tag>> GetAllTags();
        public Task<TagName> GetTagNameFromId(int id);
        public void CreateNewTagName(string tag);
        public void CreateNewTagToEvent(Event e, TagName tag);
        public void DeleteTagName(int id);
        public void DeleteTag(Event e, string tag);
    }
}
