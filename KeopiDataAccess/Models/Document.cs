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
    public abstract class Document : IDocument
    {
        [BsonElement("_id")]
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
