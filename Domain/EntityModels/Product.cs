﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityModels
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime DateAdded { get; set; }

        public bool Active { get; set; } = true;
    }
}
