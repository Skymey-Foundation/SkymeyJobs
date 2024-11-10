using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.IdGenerators;

namespace SkymeyJobsLibs.Models.Crypto.Tokens
{
    public class API_TOKEN
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        [JsonPropertyName("_id")]
        public ObjectId? _id { get; set; }

        [JsonPropertyName("Symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; } = "";

        // Текущая цена токена в USD
        [JsonPropertyName("CurrentPriceUsd")]
        public decimal CurrentPriceUsd { get; set; } = 0;

        // Рыночная капитализация токена в USD
        [JsonPropertyName("MarketCapUsd")]
        public decimal MarketCapUsd { get; set; } = 0;

        // Объем торгов за последние 24 часа в USD
        [JsonPropertyName("Volume24hUsd")]
        public decimal Volume24hUsd { get; set; } = 0;

        // Изменение цены за последние 24 часа в процентах
        [JsonPropertyName("PriceChange24h")]
        public decimal PriceChange24h { get; set; } = 0;

        // Общее количество монет (максимальное предложение)
        [JsonPropertyName("TotalSupply")]
        public decimal TotalSupply { get; set; } = 0;

        // Количество монет, находящихся в обращении
        [JsonPropertyName("CirculatingSupply")]
        public decimal CirculatingSupply { get; set; } = 0;

        // Количество монет, находящихся в обращении
        [JsonPropertyName("MaxSupply")]
        public decimal MaxSupply { get; set; } = 0;

        // Время последнего обновления данных
        [JsonPropertyName("LastUpdated")]
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        // Платформа. Блокчейн
        [JsonPropertyName("Platform")]
        public List<PlatformDB>? Platform { get; set; } = new List<PlatformDB>() { };
    }

    public class PlatformDB
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        [JsonPropertyName("_id")]
        public ObjectId? _id { get; set; }

        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("Slug")]
        public string? Slug { get; set; }

        [JsonPropertyName("Token_address")]
        public string? Token_address { get; set; }
    }
}
