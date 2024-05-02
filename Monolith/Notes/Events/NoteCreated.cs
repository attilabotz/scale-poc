using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Events
{
    public class NoteCreated
    {
        public string PublicId { get; set; }
        public string Title { get; set; }
    }
}
