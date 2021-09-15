using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Models
{
    public class Event
    {
        public string Id { get; set; }
        public string CafeBarId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Performer { get; set; }
        public string Price { get; set; }
        public int Type { get; set; }
    }
}
