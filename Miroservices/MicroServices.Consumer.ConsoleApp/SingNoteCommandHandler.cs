using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paramore.Brighter;

namespace MicroServices.Consumer.ConsoleApp
{
    public class SingNoteCommandHandler : RequestHandler<SignNoteCommand>
    {
        public override SignNoteCommand Handle(SignNoteCommand command)
        {
            Console.WriteLine($"[{DateTime.UtcNow:s}] Signing note: {command.NotePublicId}, title: {command.NoteTitle}");
            return command;
        }
    }
}
