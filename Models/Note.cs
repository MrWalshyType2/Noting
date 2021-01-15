using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Noting.Models
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public ICollection<Keyword> Keywords { get; set; }
        public string Topic { get; set; }
        public string Subtopic { get; set; }
        public SpacedRepetitionHistory SpacedRepetitionHistory { get; set; }
        public bool AutomaticIdLinking { get; set; }

        //[Column("LinkedNotes")]
        //public ICollection<Note> LinkedNotes { get; set; }

        public ICollection<NoteRelation> Children { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastModified { get; set; }
    }
}
