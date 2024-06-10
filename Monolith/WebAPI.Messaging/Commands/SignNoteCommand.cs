using System;
using Paramore.Brighter;

namespace WebAPI.Messaging.Commands
{
    public class SignNoteCommand : Request
    {
        public string NoteTitle { get; set; }
        public string NotePublicId { get; set; }
        public SignNoteCommand(ReplyAddress sendersAddress) : base(sendersAddress) { }

        public SignNoteCommand(string publicId, string title) : base(new ReplyAddress("note-signed", Guid.NewGuid()))
        {
            this.NotePublicId = publicId;
            this.NoteTitle = title;
        }

        public SignNoteCommand(string publicId, string title, ReplyAddress sendersAddress) : base(sendersAddress)
        {
            this.NotePublicId = publicId;
            this.NoteTitle = title;
        }
    }
}