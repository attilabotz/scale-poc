namespace MicroServices.NoteSigner.DataAccess
{
    public class Note
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string NoteId { get; set; }
        public string? Signature { get; set; }
    }
}