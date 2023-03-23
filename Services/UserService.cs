using hht.API.Models;
using hht.API.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace hht.API.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;
        private readonly AlertService _alertService;
        private readonly UserGameService _userGameService;

        public UserService(IUserRepository userRepo, IConfiguration config, AlertService alertService, UserGameService userGameService)
        {
            _userRepo = userRepo;
            _config = config;
            _alertService = alertService;
            _userGameService = userGameService;
        }
        public async Task<Models.User> RegisterUser(string firstName, string lastName, string email, string password, string phone, string location)
        {
            var newUser = await _userRepo.Create(firstName, lastName, email, password, phone, location);
            await _alertService.SetupDefaults(newUser._id);
            await _userGameService.SetupDefaults(newUser._id);
            var registeredUser = ConvertToModel(newUser);
            return registeredUser;
        }

        public async Task<LoggedInUser> Login(string username, string password)
        {
            var loggedInUser = new LoggedInUser();
            var userFromRepo = await _userRepo.Login(username, password);
            if (userFromRepo != null)
            {
                loggedInUser.User = ConvertToModel(userFromRepo);
                loggedInUser.Token = CreateToken(userFromRepo);
            }
            return loggedInUser;
        }

        private SecurityToken CreateToken(Repositories.User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user._id),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }

        public async Task<Models.User> FindUserByEmail(string email)
        {
            var user = await _userRepo.Find(email);
            return ConvertToModel(user);
        }

        public async Task<Models.User> FindUserById(string id)
        {
            var user = await _userRepo.FindById(id);
            return ConvertToModel(user);
        }

        private Models.User ConvertToModel(Repositories.User newUser)
        {
            return new Models.User
            {
                _id = newUser._id,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Phone = newUser.Phone,
                Location = newUser.Location
            };
        }
    }

    public class LoggedInUser
    {
        public Models.User User { get; set; }
        public SecurityToken Token { get; set; }
    }
}
