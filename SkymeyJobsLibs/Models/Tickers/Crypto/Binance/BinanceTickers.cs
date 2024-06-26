﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.IdGenerators;

namespace SkymeyJobsLibs.Models.Tickers.Crypto.Binance
{
    public class BinanceTickers
    {
        public string timezone { get; set; }
        public long serverTime { get; set; }
        public List<RateLimit> rateLimits { get; set; }
        public List<object> exchangeFilters { get; set; }
        public List<Symbol> symbols { get; set; }
    }
    public class Filter
    {
        public string filterType { get; set; }
        public string minPrice { get; set; }
        public string maxPrice { get; set; }
        public string tickSize { get; set; }
        public string minQty { get; set; }
        public string maxQty { get; set; }
        public string stepSize { get; set; }
        public int? limit { get; set; }
        public int? minTrailingAboveDelta { get; set; }
        public int? maxTrailingAboveDelta { get; set; }
        public int? minTrailingBelowDelta { get; set; }
        public int? maxTrailingBelowDelta { get; set; }
        public string bidMultiplierUp { get; set; }
        public string bidMultiplierDown { get; set; }
        public string askMultiplierUp { get; set; }
        public string askMultiplierDown { get; set; }
        public int? avgPriceMins { get; set; }
        public string minNotional { get; set; }
        public bool? applyMinToMarket { get; set; }
        public string maxNotional { get; set; }
        public bool? applyMaxToMarket { get; set; }
        public int? maxNumOrders { get; set; }
        public int? maxNumAlgoOrders { get; set; }
    }

    public class RateLimit
    {
        public string rateLimitType { get; set; }
        public string interval { get; set; }
        public int intervalNum { get; set; }
        public int limit { get; set; }
    }
    public class Symbol
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        [JsonPropertyName("_id")]
        public ObjectId? _id { get; set; }
        public string symbol { get; set; }
        public string status { get; set; }
        public string baseAsset { get; set; }
        public int baseAssetPrecision { get; set; }
        public string quoteAsset { get; set; }
        public int quotePrecision { get; set; }
        public int quoteAssetPrecision { get; set; }
        public int baseCommissionPrecision { get; set; }
        public int quoteCommissionPrecision { get; set; }
        public List<string> orderTypes { get; set; }
        public bool icebergAllowed { get; set; }
        public bool ocoAllowed { get; set; }
        public bool quoteOrderQtyMarketAllowed { get; set; }
        public bool allowTrailingStop { get; set; }
        public bool cancelReplaceAllowed { get; set; }
        public bool isSpotTradingAllowed { get; set; }
        public bool isMarginTradingAllowed { get; set; }
        public List<Filter> filters { get; set; }
        public List<string> permissions { get; set; }
        public string defaultSelfTradePreventionMode { get; set; }
        public List<string> allowedSelfTradePreventionModes { get; set; }
        public DateTime Update { get; set; }
    }
}
