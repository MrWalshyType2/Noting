using Microsoft.EntityFrameworkCore;
using Noting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Data
{
    public class MvcNoteContext : DbContext
    {
        public MvcNoteContext(DbContextOptions<MvcNoteContext> options) : base(options)
        {

        }

        public DbSet<Note> Note { get; set; }
    }
}
