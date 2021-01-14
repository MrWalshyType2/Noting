using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Noting.Models
{
    public class Note
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public ICollection<Keyword> Keywords { get; set; }
        public string Topic { get; set; }
        public string Subtopic { get; set; }
        //public SpacedRepetitionHistory SpacedRepetitionHistory { get; set; }
        public bool AutomaticIdLinking { get; set; }
        public ICollection<Note> LinkedNotes { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastModified { get; set; }
    }
}
