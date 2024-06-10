using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Paramore.Brighter;
using WebAPI.DataAccess;
using WebAPI.Messaging.Commands;
using WebAPI.Note.DTO;

namespace WebAPI.Note
{
    public class NoteManager : INoteManager
    {
        private readonly IAmACommandProcessor _commander;
        private readonly EntityFwTransactionConnectionProvider _unitOfWork;

        public NoteManager(IAmACommandProcessor commander, EntityFwTransactionConnectionProvider unitOfWork)
        {
            _commander = commander;
            _unitOfWork = unitOfWork;
        }

        public NoteDetailDto AddNewNote(NewNoteDto note)
        {
            var n = new DataAccess.Note
            {
                PublicId = Ulid.NewUlid().ToString(),
                Title = note.Title,
                CreatedAt = DateTime.Now
            };

            this._unitOfWork.Open();
            try
            {
                this._unitOfWork.Context.Notes.Add(n);
                
                this._unitOfWork.Context.SaveChanges();

                this._commander.DepositPost(new SignNoteCommand(n.PublicId, n.Title)); 
                //this._commander.ClearOutbox();

                this._unitOfWork.Close();
            }
            catch (Exception e)
            {
                this._unitOfWork.Rollback();
                throw;
            }

            return new NoteDetailDto() { PublicId = n.PublicId, Title = n.Title, CreatedAt = n.CreatedAt };
        }

        public List<NoteDetailDto> GetAllNotes()
        {
            return this._unitOfWork.Context.Notes
                .Select(n => new NoteDetailDto() { PublicId = n.PublicId, Title = n.Title, CreatedAt = n.CreatedAt})
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
        }
    }
}
