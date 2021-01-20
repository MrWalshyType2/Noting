using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models.Builders
{
    public interface INoteBuilder
    {
        INoteBuilder WithId(string id);
        INoteBuilder WithTitle(string title);
        INoteBuilder WithDescription(string description);
        INoteBuilder WithKeywords(ICollection<Keyword> keywords);
        INoteBuilder WithTopic(string topic);
        INoteBuilder WithSubtopic(string subtopic);
        INoteBuilder WithSpacedRepetitionHistory(SpacedRepetitionHistory spacedRepetitionHistory);
        INoteBuilder HasAutomaticIdLinking(bool automaticIdLinking);
        INoteBuilder DateCreatedAt(DateTime dateTime);
        INoteBuilder DateLastModified(DateTime dateTime);
        INoteBuilder HasChildren(ICollection<NoteRelation> children);
        Note BuildNote();
    }
}
