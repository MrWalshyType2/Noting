using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models.Factories.Notes
{
    public class AbstractNote
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Keyword> Keywords { get; set; }
        public string Topic { get; set; }
        public string Subtopic { get; set; }
        public SpacedRepetitionHistory SpacedRepetitionHistory { get; set; }
        public bool AutomaticIdLinking { get; set; }
        public ICollection<NoteRelation> Children { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }

    }
}
