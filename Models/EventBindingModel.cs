namespace BlogBeadando.Models
{
    public class EventBindingModel
    {
        public class CommentModel
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int TopicId { get; set; }
            public string Body { get; set; }
            public DateTime Timestamp { get; set; }
        }

        public class FavoriteTopicModel
        {
            public int UserId { get; set; }
            public int TopicId { get; set; }
        }

        public class TopicTypeModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class TopicModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int TypeId { get; set; }
            public string Description { get; set; }
        }

        public class UserModel
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
        }
    }
}
