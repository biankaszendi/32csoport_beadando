using System;
using System.IO;
using System.Linq;
using BlogBeadando.Data;
using BlogBeadando.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BCrypt.Net;

namespace BlogBeadando.Data
{

    public class DBSeeder
    {
        public static void Seed(DataContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            dbContext.Database.EnsureCreated();

            var executionStrategy = dbContext.Database.CreateExecutionStrategy();

            executionStrategy.Execute(() =>
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        if (!dbContext.Users.Any())
                        {
                            var usersData = File.ReadAllText("./Resources/users.json");
                            var parsedUsers = JsonConvert.DeserializeObject<User[]>(usersData);
                            foreach (var user in parsedUsers)
                            {
                                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                            }
                            dbContext.Users.AddRange(parsedUsers);
                            dbContext.SaveChanges();
                        }

                        if (!dbContext.Topics.Any())
                        {
                            var topicsData = File.ReadAllText("./Resources/topics.json");
                            var parsedTopics = JsonConvert.DeserializeObject<Topic[]>(topicsData);
                            dbContext.Topics.AddRange(parsedTopics);
                            dbContext.SaveChanges();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            });
        }
    }
}
