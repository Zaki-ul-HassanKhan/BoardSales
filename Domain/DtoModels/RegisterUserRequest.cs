﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DtoModels
{
    public class RegisterUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
