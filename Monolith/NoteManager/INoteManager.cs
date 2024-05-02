using NoteManager.DTO;

namespace NoteManager
{
    public interface INoteManager
    {
        void AddNewNote(NoteDto note);
    }
}
