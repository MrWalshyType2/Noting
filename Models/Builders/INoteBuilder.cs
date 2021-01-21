using Noting.Models.Factories.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models.Builders
{
    public interface INoteBuilder<T> where T:AbstractNote 
    {
        INoteBuilder<T> WithId(string id);
        INoteBuilder<T> WithTitle(string title);
        INoteBuilder<T> WithDescription(string description);
        INoteBuilder<T> WithKeywords(ICollection<Keyword> keywords);
        INoteBuilder<T> WithTopic(string topic);
        INoteBuilder<T> WithSubtopic(string subtopic);
        INoteBuilder<T> WithSpacedRepetitionHistory(SpacedRepetitionHistory spacedRepetitionHistory);
        INoteBuilder<T> HasAutomaticIdLinking(bool automaticIdLinking);
        INoteBuilder<T> DateCreatedAt(DateTime dateTime);
        INoteBuilder<T> DateLastModified(DateTime dateTime);
        INoteBuilder<T> HasChildren(ICollection<NoteRelation> children);
        T BuildNote();
    }
}
