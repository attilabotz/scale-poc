using MassTransit;
using MicroService.Producer.DataAccess;
using MicroService.Producer.WebApp.Notes.Events;

namespace MicroService.Producer.WebApp.Notes;

public class NoteManager : INoteManager
{
    private readonly NoteProducerContext _context;
    private readonly INotePublisher _notePublisher;

    public NoteManager(NoteProducerContext context, INotePublisher notePublisher)
    {
        _context = context;
        _notePublisher = notePublisher;
    }

    public void CreateNode(string text)
    {
        var note = new Note
        {
            Text = text,
            CreatedAt = DateTime.UtcNow,
            NoteId = Ulid.NewUlid().ToString()
        };
        _context.Notes.Add(note);
        _context.SaveChanges();
        
        _notePublisher.PublishNewNote(new NoteCreated()
        {
            NoteId = note.NoteId,
            Text = note.Text
        });
    }

    public IQueryable<NoteModel>? GetAllNotes()
    {
        return _context.Notes.OrderByDescending(n => n.CreatedAt).Select(n => new NoteModel
        {
            Text = n.Text, 
            CreatedAt = n.CreatedAt,
            NoteId = n.NoteId
        }).AsQueryable();
    }
}