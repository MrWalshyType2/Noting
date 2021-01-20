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
        public new string Id { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 1)]
        public new string Title { get; set; }

        [Required]
        [StringLength(20480)]
        public new string Description { get; set; }

        [NotMapped]
        public new ICollection<Keyword> Keywords { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 4)]
        public new string Topic { get; set; }
        public new string Subtopic { get; set; }

        [NotMapped]
        public new SpacedRepetitionHistory SpacedRepetitionHistory { get; set; }

        [Required]
        public new bool AutomaticIdLinking { get; set; }

        [NotMapped]
        public new ICollection<NoteRelation> Children { get; set; }

        [DataType(DataType.Date)]
        public new DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public new DateTime LastModified { get; set; }

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
