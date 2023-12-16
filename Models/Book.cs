using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryProject.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Titel { get; set; }
        public int AmountOfPages { get; set; }
        public DateTime Published { get; set; }
        public int? AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual Author? Author { get; set; }

        public virtual ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();

        public string GetAuthorName()
        {
            if (this.Author == null)
            {
                return string.Empty;
            }

            return $"{this.Author.Firstname} {this.Author.Surname}";
        }
    }

}
