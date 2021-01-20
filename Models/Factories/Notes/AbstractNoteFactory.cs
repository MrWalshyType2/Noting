using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models.Factories.Notes
{
    public interface AbstractNoteFactory
    {
        public AbstractNote GetNote();
    }
}
