using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityModels
{
    public class FinSetup
    {
        [Key]
        public int Id { get; set; }
        public string FinSteupName { get; set; }
        public DateTime DateAdded { get; set; }

        public bool Active { get; set; } = true;
    }
}
