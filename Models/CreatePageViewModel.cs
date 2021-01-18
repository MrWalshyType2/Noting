using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models
{
    public class CreatePageViewModel
    {
        public ICollection<string> Keywords { get; set; }
        public Note Note { get; set; }
    }
}
