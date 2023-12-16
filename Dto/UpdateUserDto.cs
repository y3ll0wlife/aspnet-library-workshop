using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Dto
{
    public class UpdateUserDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public IFormFile ProfileImage { get; set; }
    }
}
