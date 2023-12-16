using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Dto
{
    public class AddBookDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int AmountOfPages { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public List<int> GenreIds { get; set; }
    }
}
