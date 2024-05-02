using Notes.Models;

namespace Notes
{
    public interface INotePublisher
    {
        void Publish(NoteModel note);
    }
}
