using Noting.Models.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models.Factories.Notes
{
    public interface IAbstractNoteFactory<T> where T: AbstractNote
    {
        public T GetNote();
        public T GetNote(INoteBuilder<T> builder);
        public T GetNote(INoteBuilder<T> builder, ICollection<NoteRelation> noteRelations, SpacedRepetitionHistory spacedRepetitionHistory, ICollection<Keyword> keywords);
    }
}
