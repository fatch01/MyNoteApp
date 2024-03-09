using System.ComponentModel.DataAnnotations;

namespace mynotes.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz")]
        [StringLength(30, ErrorMessage = "Username maksimum 30 karakterden oluşabilir.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Şifre en az 8 karakter olabilir.")]
        [MaxLength(16, ErrorMessage = "Şifre maksimum 16 karakterden oluşabilir.")]
        public string? Password { get; set; }

    }
}
