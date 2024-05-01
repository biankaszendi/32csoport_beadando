using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BlogBeadando.Models;

namespace BlogBeadando.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            Database.EnsureCreated();
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<FavTopic> FavTopics { get; set; }
        public DbSet<TopicType> TopicTypes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavTopic>().HasKey(ft => new { ft.UserId, ft.TopicId });


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlServerConnectionString"));
            }
        }
    }
}