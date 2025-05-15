
using System.ComponentModel.DataAnnotations;

namespace SchoolManagerment_WebAPI.Model
{
    public class User
    {
        [Key]
        public Guid Id {get; set;} = Guid.NewGuid();
        [Required]
        [MaxLength(30)]
        public string? Fullname {get; set;}
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string? Email {get; set;}
    }
}