using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.DataAccess.Helpers
{
    public class BsonCollectionAttribute : Attribute
    {
        public string CollectionName { get; }

        public BsonCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
