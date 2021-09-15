using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeopiAPI.Models
{
    public class EventViewModel
    {
        public string Id { get; set; }
        public string CafeBarId { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Performer { get; set; }
        public string Price { get; set; }
        public int Type { get; set; }
    }
}
