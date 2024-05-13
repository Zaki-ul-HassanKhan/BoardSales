using Domain.DtoModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DtoModels.UserBoards
{
    public class AddUpdateUserBoard : Request
    {
        public List<string>? ImagesPath { get; set; }
        public string? Title { get; set; }
        public int? Condition { get; set; }
        public int? BoardType { get; set; }
        public int? BoardShapers { get; set; }
        public int? FinSystem { get; set; }
        public int? FinSetup { get; set; }
        public int? SurfCraftType { get; set; }
        public int? SurfCraftWeight { get; set; }
        public string? Length { get; set; }
        public string? Width { get; set; }
        public string? Thickness { get; set; }
        public string? Volume { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
        public bool? ConsiderSwap { get; set; }
        public int? Location { get; set; }
        public bool? IsFeatured { get; set; }
        public bool? TeeamBoard { get; set; }
        public bool? Vintage { get; set; }
        public bool? IsPosted { get; set; }
        public string? FileName { get; set; }
        public int UserId { get; set; }
    }
}
