using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using BlogBeadando.Helpers;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using BlogBeadando.Data;
using BlogBeadando.Models;
using Microsoft.AspNetCore.Identity;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }


        // GET: api/Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            // check if the username exists

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == userObj.Username);

            if (user == null)
                return NotFound(new { Message = "User not found!" });

            // verify password

            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is incorrect!" });
            }

            user.Token = CreateJwtToken(user);

            return Ok(new
            {
                Token = user.Token,
                Message = "Login was successful!"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            // check username

            if (await CheckUserNameExistAsync(userObj.Username))
                return BadRequest(new { Message = "Username already exists! " });

            // Hashing the password
            userObj.Password = PasswordHasher.HashPassword(userObj.Password);

            userObj.Role = "user";
            userObj.Token = "";

            await _context.Users.AddAsync(userObj);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Successful registration!" });
        }



        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        // check if the username exists
        private async Task<bool> CheckUserNameExistAsync(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }

        // Create the token for authentication & authorization
        private string CreateJwtToken(User user)
        {
            // Initialize a new instance of JwtSecurityTokenHandler 
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // Define the security key
            var key = Encoding.ASCII.GetBytes("9f74c1da7f6e8d3b4a5fa6b0cdee5f36f0934e4884fa2c3a5e6bdf5a80c0f2e1");

            // Set up claims for the JWT token. Claims are name/value pairs that contain information about the user
            var identity = new ClaimsIdentity(new Claim[]
            {
                // Adding the user's role as a claim
                new Claim(ClaimTypes.Role, user.Role),
                // Adding the username as a claim
                new Claim(ClaimTypes.Name, user.Username)
            });

            // Setup the signing credentials using the symmetric security key and specifying the algorithm
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            // Configure the token settings including the subject (claims), expiration time, and signing credentials
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                // Token expiration set to 1 day from now
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            // Create the token based on the descriptor
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            // Serialize the JWT token to a string and return it
            return jwtTokenHandler.WriteToken(token);
        }

        /*
        [HttpGet]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }*/
    }

}