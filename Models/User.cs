using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace LibraryProject.Models
{
    public class User : IdentityUser
    {
        public int? ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public virtual Image? Image { get; set; }
    }
}
