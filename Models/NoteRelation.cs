using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models
{
    public class NoteRelation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string ParentId { get; set; }
        //public Note Parent { get; set; }

        [Required]
        public string ChildId { get; set; }

        [NotMapped]
        public Note Child { get; set; }
    }
}
