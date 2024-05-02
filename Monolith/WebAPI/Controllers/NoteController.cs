using System.Web.Http;
using Notes;
using Notes.DTO;
using WebAPI.Note;

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
            var noteDto = new NoteDto
            {
                Title = note.Title
            };

            this._noteManager.AddNewNote(noteDto);

            return this.Ok();
        }
        
    }
}