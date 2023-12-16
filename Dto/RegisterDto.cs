using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Dto
{
    public class RegisterDto
    {
        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
