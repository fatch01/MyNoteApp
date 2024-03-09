using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace mynotes.Models
{
    public class UserEditRequestModel
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string MailAddress { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "The password must be a string with the minimum length of 5.")]
        public string Password { get; set; }

    }



} 