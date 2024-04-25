using MassTransit;
using MicroService.Consumer.DataAccess;
using MicroService.Consumer.WebApp.Events;

namespace MicroService.Consumer.WebApp.Consumers;

public class NoteCreatedConsumer : IConsumer<NoteCreated>
{
    private readonly ConsumerContext _context;

    public NoteCreatedConsumer(ConsumerContext context)
    {
        _context = context;
    }
    public Task Consume(ConsumeContext<NoteCreated> context)
    {
        this._context.Notes.Add(new Note()
        {
            Text = context.Message.Text,
            NoteId = context.Message.NoteId,
            CreatedAt = DateTime.UtcNow
        });
        this._context.SaveChanges();
        return Task.CompletedTask;
    }
}