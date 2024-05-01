using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogBeadando.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User? User { get; set; }

        [ForeignKey("Topic")]
        public int TopicId { get; set; }

        public Topic? Topic { get; set; }

        [MaxLength(255)]
        public string Body { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }
    }
}
