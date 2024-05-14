using System;
using Paramore.Brighter;
using WebAPI.Messaging.Handlers;

namespace WebAPI.Messaging
{
    public class NoteHandlerFactory : IAmAHandlerFactorySync
    {
        public IHandleRequests Create(Type handlerType)
        {
            return new SignNoteCommandHandler();
        }

        public void Release(IHandleRequests handler)
        {
            
        }
    }
}