using System.ComponentModel.DataAnnotations;

namespace FilmDukkani.MVC.DTO
{
    public class RegisterDTO
    {
        public RegisterDTO()
        {
            IsEmployee = false;
        }

        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez!")]
        [MaxLength(20)]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez!")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre (tekrar) boş geçilemez!")]
        [Compare("Password")]
        [Display(Name = "Şifre (tekrar)")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Ad boş geçilemez!")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad boş geçilemez!")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Adres boş geçilemez!")]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Kredi kartı numarası boş geçilemez!")]
        [Display(Name = "Kredi kart no")]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessage = "Kart son kullanma tarihi boş geçilemez!")]
        [Display(Name = "Kart son kullanma tarihi DD/MM/YYYY")]
        public string CardExpiryDate { get; set; }

        [Required(ErrorMessage = "Cvc kodu boş geçilemez!")]
        [Display(Name = "CVC Kodu")]
        public int CvcCode { get; set; }

        [Required(ErrorMessage = "Email boş geçilemez!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen geçerli bir email adresi girin.")]
        [Display(Name = "E-Posta")]
        public string MailAddress { get; set; }

        [Required(ErrorMessage = "Telefon numarası boş geçilemez!")]
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Üyelik seçin.")]
        [Display(Name = "Üyelik Türü")]
        public int MembershipId { get; set; }

        public bool IsEmployee { get; set; }






    }
}
