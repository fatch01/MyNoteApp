using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace mynotes.Models
{
    public class NoteRequestModel
    {
        public string Description { get; set; }
        public int AccountId { get; set; }
    }


}