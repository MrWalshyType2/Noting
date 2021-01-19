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
        private SpacedRepetitionAttempt lastAttempt;

        [NotMapped]
        public SpacedRepetitionAttempt LastAttempt 
        { 
            get 
            {
                List<SpacedRepetitionAttempt> attempts = null;
                SpacedRepetitionAttempt lastAttempt = null;
                var attemptQuery = from a in SpacedRepetitionAttempts
                                   orderby a.AttemptDate descending
                                   select a;

                if (attemptQuery.Any())
                {
                    attempts = attemptQuery.ToList();
                    lastAttempt = attempts[0];
                    this.lastAttempt = lastAttempt;
                    return this.lastAttempt;
                }
                return null;
            }
            set
            {
                this.lastAttempt = value;
            }
        }
        
        [DataType(DataType.Date)]
        public DateTime NextScheduledAttempt { get; set; }

        [Editable(false)]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
    }
}
