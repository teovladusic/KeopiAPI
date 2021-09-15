using Keopi.DataAccess.Helpers;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.DataAccess.Models
{
    [BsonCollection("cities")]
    public class CityDbModel : Document
    {
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
