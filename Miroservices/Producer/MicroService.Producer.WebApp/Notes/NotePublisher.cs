using MassTransit;
using MicroService.Producer.WebApp.Notes.Events;

namespace MicroService.Producer.WebApp.Notes;

public class NotePublisher(ITopicProducer<NoteCreated> producer) : INotePublisher
{
    public void PublishNewNote(NoteCreated noteCreatedEvent)
    {
        producer.Produce(noteCreatedEvent);
    }
}