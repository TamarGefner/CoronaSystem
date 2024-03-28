using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CoronaSystem.Models
{
    public class Member
    {
        public int ID { get; set; }
        [Range(100000000, 999999999, ErrorMessage = "IdNumber must be 9 characters.")]
        [Remote(action: "VerifyIdNumber", controller: "Members", ErrorMessage = "IdNumber already exists.")]
        public int IdNumber{ get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public int Phone { get; set; }
        public int MobilePhone { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? ImageUrl { get; set; }
    }
}
