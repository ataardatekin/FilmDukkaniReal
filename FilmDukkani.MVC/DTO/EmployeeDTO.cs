using System.ComponentModel.DataAnnotations;

namespace FilmDukkani.MVC.DTO
{
    public class EmployeeDTO
    {
        [Required]
        public string UserName { get; set; }

        
        [EmailAddress]
        public string Email { get; set; }

        
        public string Password { get; set; }

        
        public string FirstName { get; set; }

        
        public string LastName { get; set; }
    }
}
