using System.Text.Json;
using Paramore.Brighter;
using WebAPI.Messaging.Events;

namespace WebAPI.Messaging.MessageMappers
{
    public class NoteSignedMessageMapper : IAmAMessageMapper<NoteSignedEvent>
    {
        public Message MapToMessage(NoteSignedEvent request)
        {
            var header = new MessageHeader(messageId: request.Id, topic: "note-signed", MessageType.MT_EVENT);
            var body = new MessageBody(JsonSerializer.Serialize(request));
            return new Message(header, body);
        }

        public NoteSignedEvent MapToRequest(Message message)
        {
            return JsonSerializer.Deserialize<NoteSignedEvent>(message.Body.Value);
        }
    }
}