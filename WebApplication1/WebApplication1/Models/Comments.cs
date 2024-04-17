using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Comments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [ForeignKey("User")]
        public int user_id { get; set; }
        public Users? Users { get; set; }

        [ForeignKey("Topic")]
        public int topic_id { get; set; }
        public Topics? Topics { get; set; }

        [MaxLength(255)]
        public string body { get; set; } = string.Empty;
        public DateTime timestamp { get; set; }
    }
}
