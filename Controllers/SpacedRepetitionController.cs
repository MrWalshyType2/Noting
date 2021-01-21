using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noting.Data;
using Noting.Models;
using Noting.Models.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Controllers
{
    public class SpacedRepetitionController : Controller
    {
        private readonly MvcNoteContext _context;

        public SpacedRepetitionController(MvcNoteContext context)
        {
            _context = context;
        }

        // GET: SpacedRepetition
        public async Task<IActionResult> Index()
        {
            // Setup five levels of note box
            ICollection<NoteBox> noteBoxes = SetupBoxes(5);

            if (!(_context.NoteBoxRelations.Any())) return View(noteBoxes);

            // Add relations to the NoteBox collection
            await AddRelationsToNoteBoxes(noteBoxes);

            return View(noteBoxes);
        }

        // GET: Attempt/:id
        public async Task<IActionResult> Attempt(string id)
        {
            if (id == null) return NotFound();
            var attempt = await _context.SpacedRepetitionAttempts.FirstOrDefaultAsync(m => m.Id == id);
            if (attempt == null) return NotFound();

            try
            {
                attempt.SpacedRepetitionHistory =
                    await _context.SpacedRepetitionHistories.FirstOrDefaultAsync(m => m.Id == attempt.SpacedRepetitionHistoryId);
            }
            catch (Exception e)
            {
            
            }

            return View(attempt);
        }

        // GET: Start/:level
        public async Task<IActionResult> Start(string level)
        {
            level = level.ToUpper();
            // Validity checks
            if (!Enum.IsDefined(typeof(Level), level)) return NotFound();
            if (!_context.NoteBoxRelations.Any()) return NotFound();

            // Get all NoteBoxRelation's where the Level matches the passed in Level
            var noteBoxRelations = await (from noteBoxRelation in _context.NoteBoxRelations
                                          where noteBoxRelation.Level == (Level) Enum.Parse(typeof(Level), level)
                                          select noteBoxRelation).ToListAsync();
            NoteBox notesBox = new NoteBox 
            { 
                Level = (Level) Enum.Parse(typeof(Level), level),
                Notes = new List<NoteBoxRelation>()
            };

            foreach (var rel in noteBoxRelations)
            {
                // Get the note
                rel.Note = await _context.Note.FirstOrDefaultAsync(m => m.Id == rel.NoteId);
                
                // Add the notes history
                rel.Note.SpacedRepetitionHistory = await GetHistoryByNoteId(rel.NoteId);
                rel.Note.SpacedRepetitionHistory.SpacedRepetitionAttempts =
                    await GetAttemptsByHistoryId(rel.Note.SpacedRepetitionHistory.Id);
                
                // Add the NoteBoxRelation to the notesBox
                notesBox.Notes.Add(rel);
            }
            return View(notesBox);
        }

        // POST: Attempt
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Attempt([Bind("Correct", "SpacedRepetitionHistoryId")] SpacedRepetitionAttempt attempt)
        {
            attempt.AttemptDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (attempt.SpacedRepetitionHistoryId == null) return RedirectToAction(nameof(Index));
                var history = await _context.SpacedRepetitionHistories
                                            .FirstOrDefaultAsync(m => m.Id == attempt.SpacedRepetitionHistoryId);
                if (history == null) return RedirectToAction(nameof(Index));

                _context.SpacedRepetitionAttempts.Add(attempt);
                await _context.SaveChangesAsync();

                return RedirectToAction("History", new { id = history.Id });
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: History/:id
        public async Task<IActionResult> History(string id)
        {
            if (id == null) return NotFound();
            var history = await _context.SpacedRepetitionHistories.FirstOrDefaultAsync(m => m.Id == id);
            if (history == null) return NotFound();

            try
            {
                history.Note = await _context.Note.FirstOrDefaultAsync(m => m.Id == history.NoteId);
                history.SpacedRepetitionAttempts = await _context.SpacedRepetitionAttempts
                                                                 .Where(m => m.SpacedRepetitionHistoryId == history.Id)
                                                                 .ToListAsync();
            }
            catch (Exception e)
            {

            }

            return View(history);
        }

        async private Task AddRelationsToNoteBoxes(ICollection<NoteBox> noteBoxes)
        {
            try
            {
                // Get all NoteBoxRelation objects in DB
                var noteBoxRelations = await (from rel in _context.NoteBoxRelations
                                              select rel).ToListAsync();

                // for each NoteBoxRelation
                for (int i = 0; i < noteBoxRelations.Count; i++)
                {
                    // Get the note for the current NoteBoxRelation
                    INoteBuilder<Note> noteBuilder = (await _context.Note.FirstOrDefaultAsync(n => n.Id == noteBoxRelations[i].NoteId)).Builder();

                    // Get the history and add to the builder
                    var h = await GetHistoryByNoteId(noteBoxRelations[i].NoteId);
                    h.SpacedRepetitionAttempts = await GetAttemptsByHistoryId(h.Id);
                    noteBuilder.WithSpacedRepetitionHistory(h);

                    // Set the Note property on the NoteBoxRelation
                    noteBoxRelations[i].Note = noteBuilder.BuildNote();

                    // for each NoteBox
                    for (int j = 0; j < noteBoxes.Count; j++)
                    {
                        // Add relation to NoteBox
                        TryAddRelationToNoteBox(noteBoxes.ElementAt(j), noteBoxRelations[i]);

                    }
                }
            }
            catch (Exception e)
            {
                noteBoxes = SetupBoxes(5);
            }
        }

        private void TryAddRelationToNoteBox(NoteBox noteBox, NoteBoxRelation noteBoxRelation)
        {
            // if the NoteBox Level matches the NoteBoxRelations Level
            if (noteBox.Level == noteBoxRelation.Level)
            {
                // add the NoteBoxRelation to the NoteBox
                noteBox.Notes
                       .Add(noteBoxRelation);
            }
        }

        private ICollection<NoteBox> SetupBoxes(int v)
        {
            var noteBoxes = new List<NoteBox>();
            for (int i = 0; i < v; i++)
            {
                noteBoxes.Add(new NoteBox { Level = (Level)i, Notes = new List<NoteBoxRelation>() });
            }
            return noteBoxes;
        }

        private protected void AddRepetitionAttempt(string noteId, SpacedRepetitionAttempt spacedRepetitionAttempt)
        {
            //_context
        }

        async private protected void UpgradeNote(string noteId)
        {
            // Update the Level of the Note
            ICollection<NoteBoxRelation> oldRelation = (from rel in _context.NoteBoxRelations
                                                        where rel.NoteId == noteId
                                                        select rel).ToList();
            var oldRel = oldRelation.SingleOrDefault();

            if (oldRel.Level < Level.FIVE)
            {
                oldRel.Level += 1;
            }
            // Remove from this box by updating
            try
            {
                _context.NoteBoxRelations.Update(oldRel);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }
        }

        async private protected void DowngradeNote(string noteId)
        {
            // Downgrade the Level of the Note to Level One
            // Update the Level of the Note
            ICollection<NoteBoxRelation> oldRelation = (from rel in _context.NoteBoxRelations
                                                        where rel.NoteId == noteId
                                                        select rel).ToList();
            var oldRel = oldRelation.SingleOrDefault();

            if (oldRel.Level == Level.ONE) return;

            oldRel.Level = Level.ONE;

            // Remove from this box by updating
            try
            {
                _context.NoteBoxRelations.Update(oldRel);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

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
    }
}
