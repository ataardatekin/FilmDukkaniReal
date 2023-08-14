using System.ComponentModel.DataAnnotations;

namespace FilmDukkani.MVC.DTO
{
    public class LoginDTO
    {
        public LoginDTO()
        {
            RememberMe = true;
        }

        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez!")]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez!")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        public string? ReturnURL { get; set; }

        public bool RememberMe { get; set; }

    }
}
