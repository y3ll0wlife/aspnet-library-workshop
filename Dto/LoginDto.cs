using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Dto
{
    public class LoginDto
    {
        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
