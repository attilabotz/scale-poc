using Paramore.Brighter;
using Paramore.Brighter.Inbox;
using Paramore.Brighter.Inbox.Attributes;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;

namespace MicroServices.Consumer.ConsoleApp
{
    public class SignNoteCommandHandler : RequestHandler<SignNoteCommand>
    {
        public SignNoteCommandHandler()
        {
            
        }

        [UseInbox(step: 1, contextKey: typeof(SignNoteCommandHandler), onceOnly: true)]
        public override SignNoteCommand Handle(SignNoteCommand command)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{DateTime.UtcNow:s}] Signing note received: {command.NotePublicId}, title: {command.NoteTitle}");
            Console.ForegroundColor = color;
            var newNote = new NewNote
            {
                PublicId = command.NotePublicId,
                Title = command.NoteTitle,
                ReceivedAt = DateTime.UtcNow
            };
            
            // Slow local process
            string signature = this.SignNoteSlowly(newNote);
            // TODO: Slow remote process
            // TODO: Multiple slow remote calls

            return base.Handle(command);
        }


        private string SignNoteSlowly(NewNote note)
        {
            if (note.Title == null)
            {
                throw new ArgumentNullException(note.Title);
            }

            byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(note.Title + note.PublicId));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }

            Thread.Sleep(5000);

            return builder.ToString();
        }

        private void SaveNote(NewNote note, string signature)
        {

        }
    }
}