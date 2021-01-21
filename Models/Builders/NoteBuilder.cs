using Noting.Models.Factories.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models.Builders
{
    public class NoteBuilder : INoteBuilder<Note>
    {
        private string _id;
        private string _title;
        private string _description;
        private ICollection<Keyword> _keywords;
        private string _topic;
        private string _subtopic;
        private SpacedRepetitionHistory _spacedRepetitionHistory;
        private bool _automaticIdLinking;
        private DateTime _createdAt;
        private DateTime _lastModified;
        private ICollection<NoteRelation> _children;

        public Note BuildNote()
        {
            return new Note
            {
                Id = _id,
                Title = _title,
                Description = _description,
                Keywords = _keywords,
                Topic = _topic,
                Subtopic = _subtopic,
                SpacedRepetitionHistory = _spacedRepetitionHistory,
                AutomaticIdLinking = _automaticIdLinking,
                CreatedAt = _createdAt,
                LastModified = _lastModified,
                Children = _children
            };
        }

        public INoteBuilder<Note> DateCreatedAt(DateTime dateTime)
        {
            _createdAt = dateTime;
            return this;
        }

        public INoteBuilder<Note> HasAutomaticIdLinking(bool automaticIdLinking)
        {
            _automaticIdLinking = automaticIdLinking;
            return this;
        }

        public INoteBuilder<Note> DateLastModified(DateTime dateTime)
        {
            _lastModified = dateTime;
            return this;
        }

        public INoteBuilder<Note> WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public INoteBuilder<Note> WithId(string id)
        {
            _id = id;
            return this;
        }

        public INoteBuilder<Note> WithKeywords(ICollection<Keyword> keywords)
        {
            _keywords = keywords;
            return this;
        }

        public INoteBuilder<Note> WithSpacedRepetitionHistory(SpacedRepetitionHistory spacedRepetitionHistory)
        {
            _spacedRepetitionHistory = spacedRepetitionHistory;
            return this;
        }

        public INoteBuilder<Note> WithSubtopic(string subtopic)
        {
            _subtopic = subtopic;
            return this;
        }

        public INoteBuilder<Note> WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public INoteBuilder<Note> WithTopic(string topic)
        {
            _topic = topic;
            return this;
        }

        public INoteBuilder<Note> HasChildren(ICollection<NoteRelation> children)
        {
            _children = children;
            return this;
        }

        public static implicit operator Note(NoteBuilder builder)
        {
            return builder.BuildNote();
        }
    }
}
