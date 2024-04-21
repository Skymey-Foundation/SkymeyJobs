using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.IdGenerators;

namespace SkymeyJobsLibs.Models.Tickers.Crypto
{
    public class CryptoTickersView
    {
        public string Ticker {  get; set; }
        public string BaseAsset { get; set; }
        public int BaseAssetPrecision { get; set; }
        public string QuoteAsset { get; set; }
        public int QuoteAssetPrecision { get; set; }
        public int IsSpot { get; set; }
        public DateTime Update { get; set; }
    }
}
