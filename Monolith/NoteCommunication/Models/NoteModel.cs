namespace NoteCommunication.Models
{
    public class NoteModel
    {
        public NoteModel(string publicId, string title)
        {
            this.PublicId = publicId;
            this.Title = title;
        }

        public string PublicId { get; set; }
        public string Title { get; set; }
    }
}
