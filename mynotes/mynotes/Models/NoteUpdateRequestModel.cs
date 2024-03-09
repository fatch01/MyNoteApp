using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace mynotes.Models
{
    public class NoteUpdateRequestModel
    {
        public string Description { get; set; }
        public int AccountId { get; set; }
        public int NotesId { get; set; }

    }


}