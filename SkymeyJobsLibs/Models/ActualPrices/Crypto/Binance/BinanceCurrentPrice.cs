using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.IdGenerators;

namespace SkymeyJobsLibs.Models.ActualPrices.Binance
{
    public class BinanceCurrentPrice
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        [JsonPropertyName("_id")]
        public ObjectId? _id { get; set; }
        public string symbol { get; set; }
        public string price { get; set; }
        public DateTime Update { get; set; }
    }
}
