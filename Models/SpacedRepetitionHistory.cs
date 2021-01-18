using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models
{
    public class SpacedRepetitionHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string NoteId { get; set; }

        [NotMapped]
        public Note Note { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Question { get; set; }

        [NotMapped]
        public ICollection<SpacedRepetitionAttempt> SpacedRepetitionAttempts { get; set; }
        
        [NotMapped]
        public SpacedRepetitionAttempt LastAttempt { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime NextScheduledAttempt { get; set; }
    }
}
