using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Noting.Data;
using Noting.Models;

namespace Noting.Controllers
{
    public class NotesController : Controller
    {
        private readonly MvcNoteContext _context;

        public NotesController(MvcNoteContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Note.ToListAsync());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            // Check if 'id' is not null, and that a 'note' can be found in the DB
            if (id == null) return NotFound();
            var note = await _context.Note
                                     .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null) return NotFound();

            // Set children and spaced repetition history
            note.Children = await GetLinkedNoteRelations(id);
            note.SpacedRepetitionHistory = await GetHistoryByNoteId(id);
            if (note.SpacedRepetitionHistory != null)
            {
                // Set the spaced repetition histories attempts
                note.SpacedRepetitionHistory.SpacedRepetitionAttempts = 
                    await GetAttemptsByHistoryId(note.SpacedRepetitionHistory.Id);
            }
            note.Keywords = await GetKeywordsByNoteId(id);

            return View(note);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Keywords, Note")] CreatePageViewModel model)
        {
            model.Note.CreatedAt = DateTime.Now;
            model.Note.LastModified = DateTime.Now;

            if (ModelState.IsValid)
            {
                var savedModel = await AddNoteToDb(model.Note);
                
                if (model.Keywords.Any())
                {
                    AddKeywordsToDbModel(model.Note, model.Keywords);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        } 

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Description,Topic,Subtopic,AutomaticIdLinking,CreatedAt,LastModified")] Note note)
        {
            if (id != note.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var note = await _context.Note.FindAsync(id);
            _context.Note.Remove(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
