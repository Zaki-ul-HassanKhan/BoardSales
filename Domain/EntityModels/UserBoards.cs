using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityModels
{
    public class UserBoards
    {
        [Key]
        public int Id { get; set; }
        public List<string> ImagesPath { get; set; }
        public int Condition { get; set; }
        public int BoardType { get; set; }
        public int BoardShapers { get; set; }
        public int FinSystem { get; set; }
        public int FinSetup { get; set; }
        public int? SurfCraftType { get; set; }
        public int? SurfCraftWeight { get; set; }
        public string Length { get; set; }
        public string? Width { get; set; }
        public string? Thickness { get; set; }
        public string? Volume { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
        public bool? ConsiderSwap { get; set; }
        public string? Location { get; set; }
        public bool? IsFeatured { get; set; }
        public bool? TeeamBoard { get; set; }
        public bool? Vintage { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsPosted { get; set; } = false;
        public bool IsSold { get; set; } = false;
        public bool Active { get; set; } = true;
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
