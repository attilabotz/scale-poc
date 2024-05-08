using System;
using Paramore.Brighter;

namespace WebAPI.Messaging.Events
{
    public class NoteSignedEvent : Event
    {
        public string Signature { get; set; }
        public string NotePublicId { get; set; }
        public string NoteTitle { get; set; }
        public NoteSignedEvent(Guid id) : base(id)
        {

        }
    }
}