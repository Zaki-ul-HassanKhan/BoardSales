using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityModels
{
    public class Identity
    {
        [Key]
        public int IdentityId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Adapter { get; set; }
        public string? Hash { get; set; }
        public byte[]? Salt { get; set; }
        public bool Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
