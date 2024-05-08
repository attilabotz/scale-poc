using System;

namespace WebAPI.Note.ApiModels
{
    public class NoteApiModel
    {
        public string PublicId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}