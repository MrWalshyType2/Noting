using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noting.Models
{
    public class SpacedRepetitionAttempt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey("SpacedRepetitionHistoryId")]
        public string SpacedRepetitionHistoryId { get; set; }

        [NotMapped]
        public SpacedRepetitionHistory SpacedRepetitionHistory { get; set; }
        
        [Required]
        [Editable(false)]
        [DataType(DataType.Date)]
        public DateTime AttemptDate { get; set; }

        [Required]
        public bool Correct { get; set; }
    }
}