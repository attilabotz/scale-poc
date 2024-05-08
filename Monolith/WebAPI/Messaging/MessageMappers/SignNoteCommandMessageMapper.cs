using System.Text.Json;
using Paramore.Brighter;
using WebAPI.Messaging.Commands;

namespace WebAPI.Messaging.MessageMappers
{
    public class SignNoteCommandMessageMapper : IAmAMessageMapper<SignNoteCommand>
    {
        public Message MapToMessage(SignNoteCommand request)
        {
            var header = new MessageHeader(messageId: request.Id, topic: "sign-note", MessageType.MT_COMMAND);
            var body = new MessageBody(JsonSerializer.Serialize(request));
            return new Message(header, body);
        }

        public SignNoteCommand MapToRequest(Message message)
        {
            return JsonSerializer.Deserialize<SignNoteCommand>(message.Body.Value);
        }
    }
}