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
        public string NoteId { get; set; }
        public Note Note { get; set; }
        public string Question { get; set; }

        public ICollection<SpacedRepetitionAttempt> SpacedRepetitionAttempts { get; set; }
        public SpacedRepetitionAttempt LastAttempt { get; set; }
        public DateTime NextScheduledAttempt { get; set; }
    }
}
