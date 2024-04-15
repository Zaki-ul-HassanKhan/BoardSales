using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DtoModels
{
    public class RegisterUserRequest
    {
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? ProfilePicture { get; set; }
        public string Platform { get; set; }

    }
}
