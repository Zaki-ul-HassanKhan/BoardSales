using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityModels
{
    public class Locations
    {
        [Key]
        public int Id { get; set; }
        public string LocationName { get; set; }
        public DateTime DateAdded { get; set; }

        public bool Active { get; set; } = true;

    }
}
