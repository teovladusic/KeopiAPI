using Keopi.DataAccess.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.DataAccess.Models
{
    [BsonCollection("cafes")]
    public class CafeBarDbModel : Document
    {
        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("billiards")]
        public bool Billiards { get; set; }

        [BsonElement("bio")]
        public string Bio { get; set; }

        [BsonElement("cityId")]
        public string CityId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("capacity")]
        public string Capacity { get; set; }

        [BsonElement("betting")]
        public bool Betting { get; set; }

        [BsonElement("latitude")]
        public double Latitude { get; set; }

        [BsonElement("longitude")]
        public double Longitude { get; set; }

        [BsonElement("areaId")]
        public string AreaId { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("music")]
        public string Music { get; set; }

        [BsonElement("dart")]
        public bool Dart { get; set; }

        [BsonElement("startingWorkTime")]
        public int StartingWorkTime { get; set; }

        [BsonElement("age")]
        public string Age { get; set; }

        [BsonElement("smoking")]
        public bool Smoking { get; set; }

        [BsonElement("picture")]
        public string Picture { get; set; }

        [BsonElement("endingWorkTime")]
        public int EndingWorkTime { get; set; }

        [BsonElement("terrace")]
        public bool Terrace { get; set; }

        [BsonElement("seed")]
        public int Seed { get; set; }

        [BsonElement("hookah")]
        public bool Hookah { get; set; }

        [BsonElement("playground")]
        public bool Playground { get; set; }

        [BsonElement("instagram")]
        public string Instagram { get; set; }

        [BsonElement("facebook")]
        public string Facebook { get; set; }

        [BsonElement("remainderValue")]
        public long RemainderValue { get; set; }
    }
}
