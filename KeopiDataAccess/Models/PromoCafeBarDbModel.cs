using Keopi.DataAccess.Helpers;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.DataAccess.Models
{
    [BsonCollection("promoBars")]
    public class PromoCafeBarDbModel : Document
    {
        [BsonElement("cafeId")]
        public string CafeId { get; set; }
    }
}
