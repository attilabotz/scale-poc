using NoteCommunication.Models;
using Paramore.Brighter;

namespace NoteCommunication
{
    public class NotePublisher : INotePublisher
    {
        private readonly IAmACommandProcessor _commandProcessor;

        public NotePublisher(IAmACommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }
        public void Publish(NoteModel note)
        {
            return;
        }
    }
}
