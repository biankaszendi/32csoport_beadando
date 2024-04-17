using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string username { get; set; } = string.Empty;

        [StringLength(255)]
        public string name { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string password { get; set; } = string.Empty;
    }
}
