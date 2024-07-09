using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.NoteSigner.ConsoleApp
{
    internal class NewNote
    {
        public string? PublicId { get; internal set; }
        public string? Title { get; internal set; }
        public DateTime ReceivedAt { get; internal set; }
    }
}
