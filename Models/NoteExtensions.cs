using Noting.Models.Factories.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models.Builders
{
    public static class NoteExtensions
    {
        public static INoteBuilder<Note> Builder(this Note note)
        {
            return new NoteBuilder()
                        .WithId(note.Id)
                        .WithTitle(note.Title)
                        .WithDescription(note.Description)
                        .WithKeywords(note.Keywords)
                        .WithTopic(note.Topic)
                        .WithSubtopic(note.Subtopic)
                        .WithSpacedRepetitionHistory(note.SpacedRepetitionHistory)
                        .HasAutomaticIdLinking(note.AutomaticIdLinking)
                        .DateCreatedAt(note.CreatedAt)
                        .DateLastModified(note.LastModified)
                        .HasChildren(note.Children);
        }
    }
}
