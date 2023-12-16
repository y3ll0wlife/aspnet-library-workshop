using LibraryProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Model;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<Author> Authors { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=PHILIPDESKTOP;initial catalog=Library;Integrated Security=SSPI;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Book>()
            .HasMany<Genre>(x => x.Genres)
            .WithMany(x => x.Books);


        modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    Firstname = "Alice",
                    Surname = "Svensson"
                },
                new Author
                {
                    Id = 2,
                    Firstname = "Bob",
                    Surname = "Johansson"
                }
            );

        modelBuilder.Entity<Genre>().HasData(
            new Genre
            {
                Id = 1,
                Name = "Horror"
            },
            new Genre
            {
                Id = 2,
                Name = "Comedy"
            }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Titel = "Bobs och Alices äventyr på Månen",
                AmountOfPages = 804,
                Published = DateTime.Today,
                AuthorId = 1,
            },
            new Book
            {
                Id = 2,
                Titel = "Grupp 9s Linkedin kopia",
                AmountOfPages = 5,
                Published = DateTime.Parse("1987-01-05"),
                AuthorId = 2,
            },
            new Book
            {
                Id = 3,
                Titel = "The Mortal Litograph",
                AmountOfPages = 544,
                Published = DateTime.Parse("1987-01-05"),
                AuthorId = 2,
            },
            new Book
            {
                Id = 4,
                Titel = "The Way of the Lost",
                AmountOfPages = 1337,
                Published = DateTime.Parse("500-01-05"),
                AuthorId = 1,
            }
        );

        base.OnModelCreating(modelBuilder);
    }

}
