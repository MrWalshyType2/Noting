using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noting.Data;
using Noting.Models;
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
                    // Set the Note property on each relation
                    noteBoxRelations[i].Note =
                        await _context.Note.FirstOrDefaultAsync(n => n.Id == noteBoxRelations[i].NoteId);

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
    }
}
