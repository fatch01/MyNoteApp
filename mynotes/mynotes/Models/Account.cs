using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace mynotes.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IsActive { get; set; }
        public int UserId { get; set; }
        public virtual Users Users { get; set; }
        public virtual ICollection<Notes> Notes { get; set; }
    }


}
