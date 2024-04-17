using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Topics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(255)]
        public string name { get; set; } = string.Empty;

        [ForeignKey("TopicType")]
        public int TopicTypeId { get; set; } 

        public TopicTypes? TopicTypes { get; set; }

        [ForeignKey("TopicType")]
        public string description { get; set; } = string.Empty;

    }
}
