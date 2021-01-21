using Noting.Models.Builders;
using Noting.Models.Factories.Notes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Noting.Models
{
    public class Note : AbstractNote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 1)]
        public override string Title { get; set; }

        [Required]
        [StringLength(20480)]
        public override string Description { get; set; }

        [NotMapped]
        public override ICollection<Keyword> Keywords { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 4)]
        public override string Topic { get; set; }
        public override string Subtopic { get; set; }

        [NotMapped]
        public override SpacedRepetitionHistory SpacedRepetitionHistory { get; set; }

        [Required]
        public override bool AutomaticIdLinking { get; set; }

        [NotMapped]
        public override ICollection<NoteRelation> Children { get; set; }

        [DataType(DataType.Date)]
        public override DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public override DateTime LastModified { get; set; }

        public static implicit operator NoteBuilder(Note note)
        {
            return (NoteBuilder)new NoteBuilder()
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
