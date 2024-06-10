using Paramore.Brighter;
using Paramore.Brighter.Inbox.Attributes;

namespace MicroServices.Consumer.ConsoleApp
{
    public class SignNoteCommandHandler : RequestHandler<SignNoteCommand>
    {
        [UseInbox(step:0, contextKey: typeof(SignNoteCommandHandler), onceOnly: false)]
        public override SignNoteCommand Handle(SignNoteCommand command)
        {
            throw new System.NotImplementedException();
            Console.WriteLine($"[{DateTime.UtcNow:s}] Signing note: {command.NotePublicId}, title: {command.NoteTitle}");
            return command;
        }
    }
}
