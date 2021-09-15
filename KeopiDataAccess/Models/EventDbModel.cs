using Keopi.DataAccess.Helpers;
using Keopi.DataAccess.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.DataAccess.Models
{
    [BsonCollection("events")]
    public class EventDbModel : Document
    {
        [BsonElement("cafeBarId")]
        public string CafeBarId { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("performer")]
        public string Performer { get; set; }

        [BsonElement("price")]
        public string Price { get; set; }

        [BsonElement("type")]
        public int Type { get; set; }
    }
}
