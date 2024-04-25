
using MicroService.Producer.WebApp.Notes.Events;

namespace MicroService.Producer.WebApp.Notes;

public interface INotePublisher
{
    void PublishNewNote(NoteCreated noteCreatedEvent);
}