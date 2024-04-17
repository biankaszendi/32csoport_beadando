using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Reflection.Emit;
using WebApplication1.Models;

namespace BlogExplorer.Data
{
  
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            Database.EnsureCreated();
            _configuration = configuration;
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Topics> Topics { get; set; }
        public DbSet<FavoriteTopics> FavoriteTopics { get; set; }
        public DbSet<TopicTypes> TopicTypes { get; set; }
        public DbSet<Comments> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteTopics>().HasKey(ft => new { ft.user_id, ft.topic_id });


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                object value = optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
