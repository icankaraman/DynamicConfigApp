using ConfigLibrary.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigLibrary.Models
{
    public class ConfigParameterModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Type")]
        public TypeEnum Type { get; set; }

        [BsonElement("Value")]
        public string Value { get; set; }

        [BsonElement("ApplicationName")]
        public string ApplicationName { get; set; }

        [BsonElement("IsActive")]
        public bool IsActive { get; set; }

        [BsonElement("CreateDate")]
        public DateTime CreateDate { get; set; }

        [BsonElement("UpdateDate")]
        public DateTime? UpdateDate { get; set; }
    }
}
