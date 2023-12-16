namespace LibraryProject.Models
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] Blob { get; set; }
        public DateTime Published { get; set; }
    }
}
