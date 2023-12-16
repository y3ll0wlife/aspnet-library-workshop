namespace LibraryProject.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
