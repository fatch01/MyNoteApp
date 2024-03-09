using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace mynotes.Models
{
    public class Notes
    {
        [Key]
        public int NotesId { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }


}