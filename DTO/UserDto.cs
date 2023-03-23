using MongoDB.Bson.Serialization.Attributes;
using System;

namespace hht.API.Models
{
    [BsonIgnoreExtraElements]
    public class UserDto
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