using NoteCommunication.Models;

namespace NoteCommunication
{
    public interface INotePublisher
    {
        void Publish(NoteModel note);
    }
}
