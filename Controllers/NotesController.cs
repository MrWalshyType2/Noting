﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Noting.Data;
using Noting.Models;
using Noting.Models.Builders;
using Noting.Services;

namespace Noting.Controllers
{
    public class NotesController : Controller
    {
        private readonly MvcNoteContext _context;
        private readonly INoteService<Note> _noteService;

        public NotesController(MvcNoteContext context, INoteService<Note> noteService)
        {
            _context = context;
            _noteService = noteService;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            return View(await _noteService.GetAll());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var note = await _noteService.GetDetails(id);

            if (note == null) return NotFound();
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
        public async Task<IActionResult> Create([Bind("Keywords, Note")] NotePageViewModel model)
        {
            model.Note.CreatedAt = DateTime.Now;
            model.Note.LastModified = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (await _noteService.CreateNote(model))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        } 

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var note = await _noteService.GetDetails(id);
            if (note == null) return NotFound();

            var model = new NotePageViewModel
            {
                Note = note,
                Keywords = note.Keywords.Any() ?
                            (from kw in note.Keywords select kw.Name).ToList()
                            :
                            new List<string>()
            };

            return View(model);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Keywords, Note")] NotePageViewModel model)
        {
            if (id != model.Note.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var noteEditSuccess = await _noteService.EditNote(id, model);
                if (noteEditSuccess == true)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model.Note);
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
