using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Noting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Models
{
    public class NoteSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var noteContext = new MvcNoteContext(
                serviceProvider.GetRequiredService<DbContextOptions<MvcNoteContext>>());

            if (noteContext.Note.Any()) return;

            noteContext.Note.AddRange(
                new Note
                {
                    Title = "Go Shopping",
                    Description = "Buy food",
                    Topic = "Shopping",
                    Subtopic = "List"
                },
                new Note
                {
                    Title = "Go Shopping 2",
                    Description = "Buy food",
                    Topic = "Shopping",
                    Subtopic = "List"
                },
                new Note
                {
                    Title = "Go Shopping",
                    Description = "Buy food",
                    Topic = "Shopping",
                    Subtopic = "List"
                }
            );

            noteContext.SaveChanges();

            var notes = from note in noteContext.Note
                        select note;

            var ids = from note in noteContext.Note
                                      select note.Id;

            noteContext.NoteRelation.AddRange(
                new NoteRelation
                {
                    ParentId = ids.ToList()[0],
                    ChildId = ids.ToList()[1]
                }
            );

            noteContext.SaveChanges();
        }
    }
}
