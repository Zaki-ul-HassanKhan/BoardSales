using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityModels
{
    public class Shapers
    {
        [Key]
        public int Id { get; set; }
        public string? ShaperName { get; set; }
        public DateTime DateAdded { get; set; }

        public bool Active { get; set; } = true;
    }
}
