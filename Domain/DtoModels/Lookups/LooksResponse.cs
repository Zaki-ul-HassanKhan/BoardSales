using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DtoModels.Lookups
{
    public class LooksResponse
    {
        public LooksResponse()
        {
            Locations = new List<ListItem<int, string>>();
            BoardTypes = new List<ListItem<int, string>>();
            Shapers = new List<ListItem<int, string>>();
            FinSystem = new List<ListItem<int, string>>();
            FinSetup = new List<ListItem<int, string>>();
        }
        public List<ListItem<int, string>> Locations { get; set; }
        public List<ListItem<int, string>> BoardTypes { get; set; }
        public List<ListItem<int, string>> Shapers { get; set; }
        public List<ListItem<int, string>> FinSystem { get; set; }
        public List<ListItem<int, string>> FinSetup { get; set; }
    }
}
