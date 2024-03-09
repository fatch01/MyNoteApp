using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace mynotes.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string MailAddress { get; set; }
        public int IsActive { get; set; } = 1;
        public int IsAdmin { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public virtual ICollection<Account> Account { get; set; }
    }



} 