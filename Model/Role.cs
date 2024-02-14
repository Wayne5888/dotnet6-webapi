using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace webapi.Model
{
    public class Role
    {
        [Key]
        public int? RoleId { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<UserRole> UserRoles{ get; set; }
    }
}