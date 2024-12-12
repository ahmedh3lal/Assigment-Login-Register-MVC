using System.ComponentModel.DataAnnotations;

namespace Assigment.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter Your Name Plz : ")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Enter Your Email Plz : ")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Enter Your Password Plz : ")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Enter Your RePassword Plz : ")]
        public string RePassword { get; set; }
        public int? Admin {  get; set; } 
    }
}
