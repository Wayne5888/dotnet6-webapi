using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace webapi.Model
{
    public class User
    {
        [Key]
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string NickName{ get; set;}

        [JsonIgnore]
        public List<UserRole> UserRoles { get; set; }
        [JsonIgnore]
        public List<Skill> Skills { get; set;}
    }
}
