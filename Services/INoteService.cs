using Noting.Models;
using Noting.Models.Factories.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Services
{
    public interface INoteService<T> where T : AbstractNote
    {
        public Task<ICollection<T>> GetAll();
        public Task<Note> GetDetails(string id);
        public Task<bool> CreateNote(NotePageViewModel model);
        public Task<bool> CreateNote(Note model);
        public Task<bool> EditNote(string id, NotePageViewModel model);
        public Task<bool> EditNote(string id, Note model);
        public Task<bool> DeleteNote(string id);
    }
}
