using hht.API.Repositories;
using hht.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace hht.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _users;
        private readonly IConfiguration _config;

        public UserController(UserService users, IConfiguration config)
        {
            _users = users;
            _config = config;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> Get(string email)
        {
            return Ok(await _users.FindUserByEmail(email));
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterRequest body)
        {

            var email = body.Email.ToLower();
            if ((await _users.FindUserByEmail(email)).Email != null)
            {
                return BadRequest("Email already registered");
            }
            var registeredUser = await _users.RegisterUser(body.FirstName, body.LastName, email, body.Password, body.Phone, body.Location);
            return CreatedAtAction(nameof(Get), new { id = email }, email);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest body)
        {
            var user = await _users.Login(body.Email.ToLower(), body.Password);
            if (user.User == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            return Ok(new
            {
                token = tokenHandler.WriteToken(user.Token)
            });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }

    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage ="You must specify a password between 6 and 20 characters.")]
        public string Password { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string Location { get; set; }
    }

    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
