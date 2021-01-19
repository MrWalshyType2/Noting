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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>(note =>
            {
                //modelBuilder.Entity<NoteRelation>()
                  //          .HasKey(x => new { x.ParentId });

                modelBuilder.Entity<NoteRelation>() // NoteRelation
                            .HasOne(x => x.Child) // HasOne Child
                            .WithMany(x => x.Children) // WithMany Children
                            .HasForeignKey(x => x.ChildId)
                            .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Note>()
                            .HasMany(x => x.Children)
                            .WithOne(x => x.Child);

                modelBuilder.Entity<SpacedRepetitionHistory>()
                            .HasMany(x => x.SpacedRepetitionAttempts)
                            .WithOne()
                            .HasForeignKey(x => x.SpacedRepetitionHistoryId)
                            .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<SpacedRepetitionAttempt>()
                            .HasOne(x => x.SpacedRepetitionHistory)
                            .WithMany(x => x.SpacedRepetitionAttempts);
                            //.HasForeignKey(x => x.SpacedRepetitionHistoryId);

                modelBuilder.Entity<Note>()
                            .HasOne(x => x.SpacedRepetitionHistory)
                            .WithOne(x => x.Note);
            });
        }

        public DbSet<Note> Note { get; set; }
        public DbSet<NoteRelation> NoteRelation { get; set; }

        public DbSet<SpacedRepetitionHistory> SpacedRepetitionHistories { get; set; }
        public DbSet<SpacedRepetitionAttempt> SpacedRepetitionAttempts { get; set; }

        public DbSet<Keyword> Keyword { get; set; }

        public DbSet<NoteBoxRelation> NoteBoxRelations { get; set; }
    }
}
