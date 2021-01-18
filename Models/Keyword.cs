using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models
{
    public class Keyword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey("Note")]
        public string NoteId { get; set; }

        [NotMapped]
        public Note Note { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
