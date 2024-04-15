using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DtoModels
{
    public class User :Response
    {
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
        public DateTime LastLogin { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
    }
}
