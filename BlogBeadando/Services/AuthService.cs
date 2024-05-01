using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BlogBeadando.Data;
using BlogBeadando.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;

namespace BlogBeadando.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;

        public AuthService(IConfiguration configuration, DataContext dataContext)
        {
            _configuration = configuration;
            _dataContext = dataContext;
        }

        public string GenerateJwtToken(string username, string role)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool IsAuthenticated(string username, string password)
        {
            var user = GetByUsername(username);
            return user != null && BC.Verify(password, user.Password);
        }

        public bool DoesUserExist(string username)
        {
            return _dataContext.Users.Any(x => x.Username == username);
        }

        public User GetById(int id)
        {
            return _dataContext.Users.FirstOrDefault(c => c.UserId == id);
        }

        public User GetByUsername(string username)
        {
            return _dataContext.Users.FirstOrDefault(c => c.Username == username);
        }

        public User RegisterUser(User model)
        {
            var id = GenerateUniqueUserId();
            model.UserId = id;
            model.Password = BC.HashPassword(model.Password);

            var userEntity = _dataContext.Users.Add(model);
            _dataContext.SaveChanges();

            return userEntity.Entity;
        }
        private int GenerateUniqueUserId()
        {
            int id;
            var random = new Random();

            do
            {
                id = random.Next(1000000000, int.MaxValue);
            } while (GetById(id) != null);

            return id;
        }
    }


}
