using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Noting.Data;

namespace Noting.Models
{
    public class NoteBox
    {

        public NoteBox()
        {

        }

        [NotMapped]
        public ICollection<NoteBoxRelation> Notes { get; set; }

        public Level Level { get; set; }

    }

    public enum Level
    {
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE
    }
}
