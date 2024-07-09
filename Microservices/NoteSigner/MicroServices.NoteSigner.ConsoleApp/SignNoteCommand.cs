using System.Drawing;
using Paramore.Brighter;

namespace MicroServices.NoteSigner.ConsoleApp
{
    public class SignNoteCommand : Command
    {
        public string? NoteTitle { get; set; }
        public string? NotePublicId { get; set; }
        public ReplyAddress ReplyAddress { get; set; }
        public SignNoteCommand() : base(Guid.NewGuid()) { }

        public SignNoteCommand(Guid id) : base(id) { }

        public SignNoteCommand(string publicId, string title) : base(Guid.NewGuid())
        {
            NotePublicId = publicId;
            NoteTitle = title;
        }
    }

}
