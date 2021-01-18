using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Noting.Models
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [StringLength(20480)]
        public string Description { get; set; }

        public ICollection<Keyword> Keywords { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 4)]
        public string Topic { get; set; }
        public string Subtopic { get; set; }

        [NotMapped]
        public SpacedRepetitionHistory SpacedRepetitionHistory { get; set; }

        [Required]
        public bool AutomaticIdLinking { get; set; }

        [NotMapped]
        public ICollection<NoteRelation> Children { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastModified { get; set; }
    }
}
