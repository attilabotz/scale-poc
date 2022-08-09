using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MonolithFw47.Models
{
    public class MessageViewModel
    {
        [Required]
        public string Message { get; set; }
    }
}