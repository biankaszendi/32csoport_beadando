using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogBeadando.Models
{
    public class Topic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TopicId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("TopicType")]
        public int TopicTypeId { get; set; }

        public TopicType? TopicType { get; set; }

        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;
    }
}
