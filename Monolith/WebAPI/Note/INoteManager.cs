using System.Collections.Generic;
using WebAPI.Note.DTO;

namespace WebAPI.Note
{
    public interface INoteManager
    {
        NoteDetailDto AddNewNote(NewNoteDto note);
        List<NoteDetailDto> GetAllNotes();
    }
}
