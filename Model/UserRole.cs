using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace webapi.Model
{
    public class UserRole
    {
        public int UserId { get; set; }
         [JsonIgnore]
        public User User{get;set;}
        public int RoleId {get;set;}
         [JsonIgnore]
        public Role Role {get;set;}
    }
}