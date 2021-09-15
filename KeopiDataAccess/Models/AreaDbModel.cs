using Keopi.DataAccess.Helpers;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.DataAccess.Models
{
    [BsonCollection("areas")]
    public class AreaDbModel : Document
    {
        [BsonElement("cityId")]
        public string CityId { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
