using System.ComponentModel.DataAnnotations;

namespace mynotes.Models
{
    public class SingupViewModel
    {
        [Required(ErrorMessage = "Lütfen adınızı giriniz")]
        [StringLength(20, ErrorMessage = "Ad, maksimum 20 karakterden oluşabilir.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz")]
        [StringLength(20, ErrorMessage = "Soyadı maksimum 20 karakterden oluşabilir.")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Lütfen mail adresinizi giriniz")]
        [StringLength(50, ErrorMessage = "MailAddress maksimum 50 karakterden oluşabilir.")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@gmail\.com$", ErrorMessage = "Geçerli bir gmail adresi olmalıdır")]
        public string MailAddress { get; set; }

        [Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz")]
        [StringLength(30, ErrorMessage = "Username maksimum 30 karakterden oluşabilir.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Şifre en az 8 karakter olabilir.")]
        [MaxLength(16, ErrorMessage = "Şifre maksimum 16 karakter olabilir.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakterden oluşmalıdır")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen şifre tekrarınızı giriniz")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Yeniden Şifre en az 8 karakter olabilir.")]
        [MaxLength(16, ErrorMessage = "Yeniden Şifre en fazla 16 karakter olabilir.")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }
    }
}
