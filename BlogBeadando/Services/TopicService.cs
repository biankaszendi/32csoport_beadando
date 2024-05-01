using BlogBeadando.Data;
using BlogBeadando.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static BlogBeadando.Models.EventBindingModel;

namespace BlogBeadando.Services
{
    public class TopicService
    {
        private readonly DataContext _dataContext;

        public TopicService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<TopicModel> GetAvailableTopics()
        {
            return _dataContext.Topics
                .Include(t => t.TopicTypeId)
                .Select(t => new TopicModel
                {
                    Id = t.TopicId,
                    Name = t.Name,
                    TypeId = t.TopicTypeId,
                    Description = t.Description
                })
                .ToList();
        }

        public IEnumerable<CommentModel> GetCommentsForTopic(int topicId)
        {
            return _dataContext.Comments
                .Where(c => c.TopicId == topicId)
                .Select(c => new CommentModel
                {
                    Id = c.CommentId,
                    UserId = c.UserId,
                    TopicId = c.TopicId,
                    Body = c.Body,
                    Timestamp = c.Timestamp
                })
                .ToList();
        }

        public void AddComment(CommentModel comment)
        {
            var newComment = new Comment
            {
                UserId = comment.UserId,
                TopicId = comment.TopicId,
                Body = comment.Body,
                Timestamp = DateTime.Now
            };

            _dataContext.Comments.Add(newComment);
            _dataContext.SaveChanges();
        }

        public Topic Update(TopicInputModel model)
        {
            var existingTopic = _dataContext.Topics.FirstOrDefault(t => t.TopicId == model.TopicId);

            if (existingTopic == null)
            {
                return null;
            }

            existingTopic.Name = model.Name;
            existingTopic.Description = model.Description;

            _dataContext.SaveChanges();

            return existingTopic;
        }
    }
}
