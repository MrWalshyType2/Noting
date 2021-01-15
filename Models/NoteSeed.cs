﻿using Microsoft.EntityFrameworkCore;
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

            noteContext.NoteRelation.AddRange(
                new NoteRelation
                {
                    ParentId = projectNotes[0].Id,
                    ChildId = projectNotes[1].Id
                },
                new NoteRelation
                {
                    ParentId = projectNotes[0].Id,
                    ChildId = projectNotes[2].Id
                },
                new NoteRelation
                {
                    ParentId = projectNotes[1].Id,
                    ChildId = projectNotes[0].Id
                },
                new NoteRelation
                {
                    ParentId = projectNotes[1].Id,
                    ChildId = projectNotes[2].Id
                },
                new NoteRelation
                {
                    ParentId = projectNotes[2].Id,
                    ChildId = projectNotes[0].Id
                },
                new NoteRelation
                {
                    ParentId = projectNotes[2].Id,
                    ChildId = projectNotes[1].Id
                }
            );
            await noteContext.SaveChangesAsync();
            noteContextTransaction.Commit();
        }
    }
}
