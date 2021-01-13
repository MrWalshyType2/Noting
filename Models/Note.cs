using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Noting.Models
{
    public class Note : AbstractNote<Note>
    {
        public void Log()
        {
            Console.WriteLine(ToString());
        }
    }
}
