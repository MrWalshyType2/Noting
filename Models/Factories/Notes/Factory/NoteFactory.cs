using Noting.Models.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models.Factories.Notes.Factory
{
    public class NoteFactory : AbstractNoteFactory
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

        public AbstractNote GetNote()
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
    }
}
