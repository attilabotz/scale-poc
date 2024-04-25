namespace MicroService.Consumer.WebApp.Events;

public record NoteCreated
{
    public string NoteId { get; set; }
    public string Text { get; set; }
}