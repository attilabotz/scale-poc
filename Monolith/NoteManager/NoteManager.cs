using System;
using DataAccess.Note;
using NoteCommunication;
using NoteCommunication.Models;
using NoteManager.DTO;

namespace NoteManager
{
    public class NoteManager : INoteManager
    {
        private readonly NoteContext _context;
        private readonly INotePublisher _publisher;

        public NoteManager(NoteContext context, INotePublisher publisher)
        {
            _context = context;
            _publisher = publisher;
        }

        public void AddNewNote(NoteDto note)
        {
            var n = new Note
            {
                PublicId = Guid.NewGuid().ToString(),
                Title = note.Title,
                CreatedAt = DateTime.Now
            };
            
            this._context.Notes.Add(n);
            
            this._context.SaveChanges();

            this._publisher.Publish(new NoteModel(n.PublicId, n.Title));
            
        }
    }
}
