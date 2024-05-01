using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogBeadando.Models
{
    public class TopicType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TopicTypeId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
    }
}
