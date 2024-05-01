using System.ComponentModel.DataAnnotations;

namespace BlogBeadando.Models
{
    public class TopicInputModel
    {
        public int TopicId { get; set; } 

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
