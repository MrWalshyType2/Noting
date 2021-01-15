using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            if (id == null) return NotFound();

            var note = await _context.Note
                                     .FirstOrDefaultAsync(m => m.Id == id);

            if (note == null) return NotFound();

            note.Children = await GetLinkedNoteRelations(id);
            note.SpacedRepetitionHistory = await GetHistoryByNoteId(id);
            if (note.SpacedRepetitionHistory != null)
            {
                note.SpacedRepetitionHistory.SpacedRepetitionAttempts = await GetAttemptsByHistoryId(id);
            }
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
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Topic,Subtopic,AutomaticIdLinking,CreatedAt,LastModified")] Note note)
        {
            if (ModelState.IsValid)
            {
                _context.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(note);
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
            var relations = from noteRel in _context.NoteRelation
                            where noteRel.ParentId == id
                            join n in _context.Note on noteRel.ChildId equals n.Id
                            select new NoteRelation { Child = n, ChildId = noteRel.ChildId, ParentId = noteRel.ParentId, Id = noteRel.Id };
            
            return await relations.ToListAsync();
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
    }
}
