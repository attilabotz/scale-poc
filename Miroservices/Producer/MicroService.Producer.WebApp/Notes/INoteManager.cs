namespace MicroService.Producer.WebApp.Notes;

public interface INoteManager
{
    void CreateNode(string text);
    IQueryable<NoteModel>? GetAllNotes();
}