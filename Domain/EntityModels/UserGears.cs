using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityModels
{
    public class UserGears
    {
        [Key]
        public int Id { get; set; }
        public List<string> ImagesPath { get; set; }
        public int Condition { get; set; }
        public string? Price { get; set; }
        public string? Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsPosted { get; set; } = false;
        public bool IsSold { get; set; } = false;
        public bool Active { get; set; } = true;
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
