using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Models
{
    public class PromoCafe
    {
        [BsonElement("_id")]
        public string Id { get; set; }
        [BsonElement("cafeId")]
        public string CafeId { get; set; }
    }
}
