using Domain.DtoModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DtoModels.UpdateUser
{
    public class UpdateUserResponse:Response
    {
        public string? Name { get; set; }
        public int? Location { get; set; }
        public string? ProfilePicture { get; set; }
        public int? BoardLength { get; set; }
        public int? BoardType { get; set; }
        public int? Distance { get; set; }
        public string? FileName { get; set; }
        public bool? GetStartedCompleted { get; set; }
    }
}
