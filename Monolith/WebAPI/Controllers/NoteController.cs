using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web.Http;
using Paramore.Brighter;
using WebAPI.Messaging.Commands;
using WebAPI.Note;
using WebAPI.Note.ApiModels;
using WebAPI.Note.DTO;

namespace WebAPI.Controllers
{
    public class NoteController : ApiController
    {
        private readonly INoteManager _noteManager;

        public NoteController(INoteManager noteManager)
        {
            _noteManager = noteManager;
        }
        public class NoteRequest
        {
            public string Title { get; set; }
        }

        [HttpPost]
        public IHttpActionResult Create(NoteRequest note)
        {
            var noteDto = new NewNoteDto
            {
                Title = note.Title
            };

            NoteDetailDto newNote = this._noteManager.AddNewNote(noteDto);

            return this.Json(newNote);
        }

        [HttpGet]
        public IHttpActionResult List()
        {
            List<NoteDetailDto> notes = this._noteManager.GetAllNotes();
            List<NoteApiModel> noteApiModels = notes
                .Select(n => new NoteApiModel
                {
                    PublicId = n.PublicId,
                    Title = n.Title,
                    CreatedAt = n.CreatedAt
                }).ToList();

            return this.Json(noteApiModels);
        }


    }
}