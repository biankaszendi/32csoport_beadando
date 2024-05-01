using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogBeadando.Models
{
    public class FavTopic
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User? User { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Topic")]
        public int TopicId { get; set; }

        public Topic? Topic { get; set; }
    }
}
