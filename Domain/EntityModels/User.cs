using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityModels
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? LocationName { get; set; }
        public string? Location { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool Active { get; set; } = true;
        public bool Verified { get; set; } = true;
        public string? VerificationCode { get; set; }
        public int? BoardType { get; set; }
        public int? BoardLength { get; set; }
        public int? Distance { get; set; }
        public bool? GetStartedCompleted { get; set; }
        public DateTime LastLogin { get; set; } = DateTime.Now;
        public string? Role { get; set; } = "USER";
    }
}
