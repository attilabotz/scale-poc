using Paramore.Brighter;

namespace MicroServices.Consumer.ConsoleApp
{
    public class SignNoteCommand : Command
    {
        public string? NoteTitle { get; set; }
        public string? NotePublicId { get; set; }

        public SignNoteCommand() : base(Guid.NewGuid()) { }
        public SignNoteCommand(Guid id) : base(id) { }

        public SignNoteCommand(string publicId, string title) : base(Guid.NewGuid())
        {
            this.NotePublicId = publicId;
            this.NoteTitle = title;
        }
    }
}
