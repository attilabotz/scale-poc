using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Note.DTO
{
    public class NoteDetailDto
    {
        public string PublicId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}