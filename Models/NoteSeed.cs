using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Noting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Noting.Models
{
    public class NoteSeed
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using var noteContext = new MvcNoteContext(
                serviceProvider.GetRequiredService<DbContextOptions<MvcNoteContext>>());

            using IDbContextTransaction noteContextTransaction = await noteContext.Database.BeginTransactionAsync();

            if (await noteContext.Note.AnyAsync()) return;

            noteContext.Note.AddRange(
                new Note
                {
                    Title = "Create documentation",
                    Description = "Create documentation for project xxxx",
                    Topic = "Projects",
                    Subtopic = "xxxx"
                },
                new Note
                {
                    Title = "Initialise project",
                    Description = "Initialise project with xxxx",
                    Topic = "Projects",
                    Subtopic = "xxxx"
                },
                new Note
                {
                    Title = "Add model",
                    Description = "Add xxxx model",
                    Topic = "Projects",
                    Subtopic = "xxxx"
                },
                new Note
                {
                    Title = "Go Shopping",
                    Description = "Buy food",
                    Topic = "Shopping",
                    Subtopic = "List"
                }
            );

            await noteContext.SaveChangesAsync();

            var notes = from note in noteContext.Note
                        select note;


            var projectNotes = (from note in notes
                               where note.Topic == "Projects"
                               select note).ToList();

            var targetNote = projectNotes[0];

            noteContext.NoteRelation.AddRange(
                new NoteRelation
                {
                    ParentId = targetNote.Id,
                    ChildId = projectNotes[1].Id
                },
                new NoteRelation
                {
                    ParentId = targetNote.Id,
                    ChildId = projectNotes[2].Id
                },
                new NoteRelation
                {
                    ParentId = projectNotes[1].Id,
                    ChildId = targetNote.Id
                },
                new NoteRelation
                {
                    ParentId = projectNotes[1].Id,
                    ChildId = projectNotes[2].Id
                },
                new NoteRelation
                {
                    ParentId = projectNotes[2].Id,
                    ChildId = targetNote.Id
                },
                new NoteRelation
                {
                    ParentId = projectNotes[2].Id,
                    ChildId = projectNotes[1].Id
                }
            );

            await noteContext.SaveChangesAsync();

            noteContext.SpacedRepetitionHistories.Add(
                new SpacedRepetitionHistory
                {
                    NoteId = targetNote.Id,
                    NextScheduledAttempt = DateTime.Now.Date,
                    Question = "What is the x of n?",
                    CreatedAt = DateTime.Parse("01/01/2021")
                }
            );

            await noteContext.SaveChangesAsync();

            var noteRepHistQuery = from n in noteContext.SpacedRepetitionHistories
                                   where n.NoteId == targetNote.Id
                                   select n;

            var noteRepHist = (await noteRepHistQuery.ToListAsync())[0];

            noteContext.SpacedRepetitionAttempts.AddRange(
                new SpacedRepetitionAttempt
                {
                    SpacedRepetitionHistoryId = noteRepHist.Id,
                    AttemptDate = DateTime.Parse("03/01/2021"),
                    Correct = false
                },
                new SpacedRepetitionAttempt
                {
                    SpacedRepetitionHistoryId = noteRepHist.Id,
                    AttemptDate = DateTime.Parse("07/01/2021"),
                    Correct = true
                }
            );

            await noteContext.SaveChangesAsync();

            noteContext.Keyword.AddRange(
                new Keyword
                {
                    Name = "Project",
                    NoteId = targetNote.Id
                },
                new Keyword
                {
                    Name = "Work",
                    NoteId = targetNote.Id
                },
                new Keyword
                {
                    Name = "C#",
                    NoteId = targetNote.Id
                }
            );

            await noteContext.SaveChangesAsync();

            noteContextTransaction.Commit();
        }
    }
}
