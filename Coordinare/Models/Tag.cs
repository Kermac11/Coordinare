using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coordinare.Models
{
    public class Tag
    {
        public Tag(int tagId, int eVentId, string tagName)
        {
            Tag_ID = tagId;
            Event_ID = eVentId;
            TagName = tagName;
        }
        public int Tag_ID { get; set; }
        public int Event_ID { get; set; }
        public string TagName { get; set; }
    }
}
