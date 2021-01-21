using Noting.Models.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models.Factories.Notes.Factory
{
    public class NoteFactory : IAbstractNoteFactory<Note>
    {
        private readonly string _id;
        private readonly string _title;
        private readonly string _description;
        private readonly ICollection<Keyword> _keywords;
        private readonly string _topic;
        private readonly string _subtopic;
        private readonly SpacedRepetitionHistory _spacedRepetitionHistory;
        private readonly bool _automaticIdLinking;
        private readonly DateTime _createdAt;
        private readonly DateTime _lastModified;
        private readonly ICollection<NoteRelation> _children;

        public NoteFactory()
        {
        }

        public NoteFactory(NotePageViewModel model)
        {
            _id = model.Note.Id;
            _title = model.Note.Title;
            _description = model.Note.Description;
            _keywords = model.Note.Keywords;
            _topic = model.Note.Topic;
            _subtopic = model.Note.Subtopic;
            _spacedRepetitionHistory = model.Note.SpacedRepetitionHistory;
            _automaticIdLinking = model.Note.AutomaticIdLinking;
            _createdAt = model.Note.CreatedAt;
            _lastModified = model.Note.LastModified;
            _children = model.Note.Children;
        }

        public NoteFactory(string id, string title, string description, string topic, string subtopic, DateTime createdAt, DateTime lastModified)
        {
            _id = id;
            _title = title;
            _description = description;
            _topic = topic;
            _subtopic = subtopic;
            _createdAt = createdAt;
            _lastModified = lastModified;
        }

        public NoteFactory(string id, string title, string description, ICollection<Keyword> keywords, string topic, string subtopic, SpacedRepetitionHistory spacedRepetitionHistory, bool automaticIdLinking, DateTime createdAt, DateTime lastModified, ICollection<NoteRelation> children)
        {
            _id = id;
            _title = title;
            _description = description;
            _keywords = keywords;
            _topic = topic;
            _subtopic = subtopic;
            _spacedRepetitionHistory = spacedRepetitionHistory;
            _automaticIdLinking = automaticIdLinking;
            _createdAt = createdAt;
            _lastModified = lastModified;
            _children = children;
        }

        public Note GetNote()
        {
            return new NoteBuilder()
                        .WithId(_id)
                        .WithTitle(_title)
                        .WithDescription(_description)
                        .WithKeywords(_keywords)
                        .WithTopic(_topic)
                        .WithSubtopic(_subtopic)
                        .WithSpacedRepetitionHistory(_spacedRepetitionHistory)
                        .HasAutomaticIdLinking(_automaticIdLinking)
                        .DateCreatedAt(_createdAt)
                        .DateLastModified(_lastModified)
                        .HasChildren(_children)
                        .BuildNote();
        }

        public Note GetNote(INoteBuilder<Note> builder)
        {
            return builder.BuildNote();
        }

        public Note GetNote(INoteBuilder<Note> builder, ICollection<NoteRelation> noteRelations, SpacedRepetitionHistory spacedRepetitionHistory, ICollection<Keyword> keywords)
        {
            return builder
                    .HasChildren(noteRelations)
                    .WithSpacedRepetitionHistory(spacedRepetitionHistory)
                    .WithKeywords(keywords)
                    .BuildNote();
        }
    }
}
