using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.IdGenerators;

namespace SkymeyJobsLibs.Models.Blockchain.Bitcoin
{
    public class MiningInfo
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        [JsonPropertyName("_id")]
        public ObjectId? _id { get; set; }
        public int blocks { get; set; }
        public double difficulty { get; set; }
        public double networkhashps { get; set; }
        public int pooledtx { get; set; }
        public string chain { get; set; }
        public string warnings { get; set; }
        public DateTime Update { get; set; }
    }

    public class MiningInfoQueryResult
    {
        public int blocks { get; set; }
        public double difficulty { get; set; }
        public double networkhashps { get; set; }
        public int pooledtx { get; set; }
        public string chain { get; set; }
        public string warnings { get; set; }
    }

    public class MiningInfoQuery
    {
        public MiningInfoQueryResult result { get; set; }
        public object error { get; set; }
        public object id { get; set; }
    }
}