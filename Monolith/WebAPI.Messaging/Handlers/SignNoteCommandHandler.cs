using System;
using Paramore.Brighter;
using WebAPI.Messaging.Commands;

namespace WebAPI.Messaging.Handlers
{
    public class SignNoteCommandHandler : RequestHandler<SignNoteCommand>
    {
        public override SignNoteCommand Handle(SignNoteCommand command)
        {
            Console.WriteLine($"Sign note command received with title: {command.NoteTitle}");
            return base.Handle(command);
        }
    }
}