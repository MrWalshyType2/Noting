using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Noting.Data;
using Noting.Models;
using Noting.Models.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Services
{
    public class NoteService : INoteService<Note>
    {
        private readonly MvcNoteContext _context;

        public NoteService(MvcNoteContext context)
        {
            _context = context;
        }
        public Task CreateNote(NotePageViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task CreateNote(Note model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteNote(string id)
        {
            throw new NotImplementedException();
        }

        public Task EditNote(string id, NotePageViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task EditNote(string id, Note model)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Note>> GetAll()
        {
            return await _context.Note.ToListAsync();
        }

        async public Task<Note> GetDetails(string id)
        {
            // Check if 'id' is not null, and that a 'note' can be found in the DB
            if (id == null) return null;
            NoteBuilder note = await _context.Note
                                     .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null) return null;

            // Set children
            var linkedRelations = await GetLinkedNoteRelations(id);
            note.HasChildren(linkedRelations);

            // Set spaced repetition history
            var history = await GetHistoryByNoteId(id);
            //note.WithSpacedRepetitionHistory(history); 

            // If there is a SpacedRepetitionHistory
            if (history != null)
            {
                // Set the spaced repetition histories attempts
                history.SpacedRepetitionAttempts = await GetAttemptsByHistoryId(history.Id);
                note.WithSpacedRepetitionHistory(history);
            }
            var keywords = await GetKeywordsByNoteId(id);
            note.WithKeywords(keywords);

            return note.BuildNote();
        }

        private bool NoteExists(string id)
        {
            return _context.Note.Any(e => e.Id == id);
        }

        async private Task<ICollection<NoteRelation>> GetLinkedNoteRelations(string id)
        {
            try
            {
                var relations = from noteRel in _context.NoteRelation
                                where noteRel.ParentId == id
                                join n in _context.Note on noteRel.ChildId equals n.Id
                                select new NoteRelation { Child = n, ChildId = noteRel.ChildId, ParentId = noteRel.ParentId, Id = noteRel.Id };

                return await relations.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        async private Task<ICollection<SpacedRepetitionAttempt>> GetAttemptsByHistoryId(string id)
        {
            List<SpacedRepetitionAttempt> attempts = null;
            try
            {
                if (await _context.SpacedRepetitionAttempts.AnyAsync() && id != null)
                {
                    attempts = await (from a in _context.SpacedRepetitionAttempts
                                      where a.SpacedRepetitionHistoryId == id
                                      select a).ToListAsync();
                }
            }
            catch (Exception e) { }
            return attempts;
        }

        async private Task<SpacedRepetitionHistory> GetHistoryByNoteId(string id)
        {
            List<SpacedRepetitionHistory> histories = null;
            SpacedRepetitionHistory history = null;
            try
            {
                if (await _context.SpacedRepetitionHistories.AnyAsync())
                {
                    histories = await (from h in _context.SpacedRepetitionHistories
                                       where h.NoteId == id
                                       select h).ToListAsync();
                }
                history = histories.ElementAtOrDefault(0);
            }
            catch (Exception e) { }
            return history;
        }

        async private Task<ICollection<Keyword>> GetKeywordsByNoteId(string id)
        {
            try
            {
                // Set keywords if any
                var keywords = await (from keyword in _context.Keyword
                                      where keyword.NoteId == id
                                      select keyword).ToListAsync();
                return keywords;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        async private void AddKeywordsToDbModel(Note note, ICollection<string> keywords)
        {
            using IDbContextTransaction dbContextTransaction = await _context.Database.BeginTransactionAsync();
            foreach (var k in keywords)
            {
                _context.Keyword.Add(
                    new Keyword
                    {
                        Name = k,
                        NoteId = note.Id
                    }
                );
            }
            await _context.SaveChangesAsync();
            await dbContextTransaction.CommitAsync();
        }

        async private Task<Note> AddNoteToDb(Note model)
        {
            using IDbContextTransaction dbContextTransaction = await _context.Database.BeginTransactionAsync();
            var savedNoteEntry = _context.Note.Add(model);
            await _context.SaveChangesAsync();
            await dbContextTransaction.CommitAsync();

            return model;
        }
    }
}
