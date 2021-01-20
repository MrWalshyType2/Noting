using Noting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noting.Services
{
    interface INoteService
    {
        public Task GetAll();
        public Task GetDetails(string id);
        public Task CreateNote(NotePageViewModel model);
        public Task CreateNote(Note model);
        public Task EditNote(string id, NotePageViewModel model);
        public Task EditNote(string id, Note model);
        public Task DeleteNote(string id);
    }
}
