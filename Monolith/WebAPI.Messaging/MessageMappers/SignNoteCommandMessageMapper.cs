using System;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Paramore.Brighter;
using WebAPI.Messaging.Commands;

namespace WebAPI.Messaging.MessageMappers
{
    public class SignNoteCommandMessageMapper : IAmAMessageMapper<SignNoteCommand>
    {
        public Message MapToMessage(SignNoteCommand request)
        {
            var header = new MessageHeader(
                messageId: request.Id,
                topic: "sign-note",
                MessageType.MT_COMMAND,
                replyTo: request.ReplyAddress.Topic,
                correlationId: request.ReplyAddress.CorrelationId);
            var body = new MessageBody(JsonSerializer.Serialize(request));

            return new Message(header, body);
        }

        public SignNoteCommand MapToRequest(Message message)
        {
            var replyAddress =
                new ReplyAddress(topic: message.Header.ReplyTo, correlationId: message.Header.CorrelationId);
            JObject commandBody = JObject.Parse(message.Body.Value);
            
            var command = new SignNoteCommand(
                               publicId: (string)commandBody["NotePublicId"],
                               title: (string)commandBody["NoteTitle"],
                               sendersAddress: replyAddress);
            
            command.Id = Guid.Parse((string)commandBody["Id"]);

            return command;
        }
    }
}