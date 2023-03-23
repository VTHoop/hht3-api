using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hht.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HhtDb"));
            var database = client.GetDatabase("homerhokie_bkp");
            _users = database.GetCollection<User>("users");
        }
        public async Task<User> Create(string firstName, string lastName, string email, string password, string phone, string location)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new User
            {
                _id = ObjectId.GenerateNewId().ToString(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Location = location,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedOn = DateTime.Now,
                AdminCreated = "No",
                Admin = "No",
                Prognosticator = "Yes"
            };
            await _users.InsertOneAsync(newUser);
            return newUser;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        public async Task<User> Find(string email)
        {
            var user = await (await _users.FindAsync(u => u.Email == email)).FirstOrDefaultAsync();
            if (user == null)
            {
                return new User();
            }
            return user;
        }

        public async Task<User> FindById(string id)
        {
            var user = await (await _users.FindAsync(u => u._id == id)).FirstOrDefaultAsync();
            if (user == null)
            {
                return new User();
            }
            return user;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await (await _users.FindAsync(u => u.Email == email)).FirstOrDefaultAsync();

            if (user == null)
                return null;
            //old users do not have salt
            if (user.PasswordSalt == null)
            {
                if (!VerifyPasswordHash(password, System.Text.Encoding.UTF8.GetBytes(user.Password)))
                {
                    return null;
                }
            } else
            {
                if (!VerifyPasswordHashWithSalt(password, user.PasswordHash, user.PasswordSalt))
                {
                    return null;
                }
            }
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        private bool VerifyPasswordHashWithSalt(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

    }

    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement("f_name")]
        public string FirstName { get; set; }
        [BsonElement("l_name")]
        public string LastName { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("password_hash")]
        public byte[] PasswordHash { get; set; }
        [BsonElement("password_salt")]
        public byte[] PasswordSalt { get; set; }
        [BsonElement("admin")]
        public string Admin { get; set; }
        [BsonElement("created_on")]
        //[BsonIgnoreIfNull]
        public DateTime? CreatedOn { get; set; }
        [BsonElement("updated_on")]
        [BsonIgnoreIfNull]
        public DateTime? UpdatedOn { get; set; }
        [BsonElement("phone")]
        public string Phone { get; set; }
        [BsonElement("location")]
        public string Location { get; set; }
        [BsonElement("admin_created")]
        public string AdminCreated { get; set; }
        [BsonElement("prognosticator")]
        public string Prognosticator { get; set; }
    }

}
