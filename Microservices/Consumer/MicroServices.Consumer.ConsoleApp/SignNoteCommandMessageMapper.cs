using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Paramore.Brighter;

namespace MicroServices.Consumer.ConsoleApp
{
    public class SignNoteCommandMessageMapper : IAmAMessageMapper<SignNoteCommand>
    {
        public Message MapToMessage(SignNoteCommand request)
        {
            var header = new MessageHeader(
                messageId: request.Id, 
                topic: "sign-note", 
                messageType: MessageType.MT_COMMAND,
                replyTo: request.ReplyAddress.Topic,
                correlationId: request.ReplyAddress.CorrelationId);
            var body = new MessageBody(JsonConvert.SerializeObject(request));

            var message = new Message(header, body);
            return message;
        }

        public SignNoteCommand MapToRequest(Message message)
        {
            var command = JsonConvert.DeserializeObject<SignNoteCommand>(message.Body.Value);
            return command;
        }
    }
}
