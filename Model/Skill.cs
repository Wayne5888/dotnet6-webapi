using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Model;

public class Skill
{
    [Key]
    [JsonIgnore]
    public int? SkillId {get;set;}
    public string SkillName{get;set;}
    public int UserId{get;set;}
    [JsonIgnore]
    public User User{get;set;}

}


