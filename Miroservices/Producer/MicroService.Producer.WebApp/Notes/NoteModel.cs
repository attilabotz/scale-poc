using System.ComponentModel.DataAnnotations;

namespace MicroService.Producer.WebApp.Notes;

public class NoteModel
{
    [Required] [StringLength(500)] public string Text { get; set; }

    public DateTime CreatedAt { get; set; }

    public string NoteId { get; set; }
}