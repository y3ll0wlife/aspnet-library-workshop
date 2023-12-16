using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Dto
{
    public class UpdateBookDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int AmountOfPages { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}
