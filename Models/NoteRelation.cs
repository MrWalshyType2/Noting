using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models
{
    public class NoteRelation
    {
        public string ParentId { get; set; }
        public Note Parent { get; set; }

        public string ChildId { get; set; }
        public Note Child { get; set; }
    }
}
