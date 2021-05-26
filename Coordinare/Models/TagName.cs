using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coordinare.Models
{
    public class TagName
    {
        public TagName(int tagId, string tagName)
        {
            Tag_ID = tagId;
            Name = tagName;
        }
        public int Tag_ID { get; set; }
        public string Name { get; set; }
    }
}
