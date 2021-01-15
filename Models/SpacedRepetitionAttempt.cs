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
        public string SpacedRepetitionHistoryId { get; set; }
        public SpacedRepetitionHistory SpacedRepetitionHistory { get; set; }
        public DateTime AttemptDate { get; set; }
        public bool Correct { get; set; }
    }
}