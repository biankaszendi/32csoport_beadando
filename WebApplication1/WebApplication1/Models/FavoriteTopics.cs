using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class FavoriteTopics
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("User")]
        public int user_id { get; set; }

        public Users? Users { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Topic")]
        public int topic_id { get; set; }
    }
}
