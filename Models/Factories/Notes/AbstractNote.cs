using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models.Factories.Notes
{
    public class AbstractNote
    {
        public virtual string Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual ICollection<Keyword> Keywords { get; set; }
        public virtual string Topic { get; set; }
        public virtual string Subtopic { get; set; }
        public virtual SpacedRepetitionHistory SpacedRepetitionHistory { get; set; }
        public virtual bool AutomaticIdLinking { get; set; }
        public virtual ICollection<NoteRelation> Children { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime LastModified { get; set; }

    }
}
