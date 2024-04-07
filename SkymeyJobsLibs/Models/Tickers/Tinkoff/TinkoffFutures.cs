using Google.Protobuf.WellKnownTypes;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.IdGenerators;

namespace SkymeyJobsLibs.Models.Tickers.Tinkoff
{
    public class TinkoffFuturesBasicAssetSize
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffFuturesDlong
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffFuturesDlongMin
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffFuturesDshort
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffFuturesDshortMin
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffFuturesInstrument
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        [JsonPropertyName("_id")]
        public ObjectId? _id { get; set; }
        public string figi { get; set; }
        public string ticker { get; set; }
        public string classCode { get; set; }
        public int? lot { get; set; }
        public string currency { get; set; }
        public TinkoffFuturesKlong klong { get; set; }
        public TinkoffFuturesKshort kshort { get; set; }
        public TinkoffFuturesDlong dlong { get; set; }
        public TinkoffFuturesDshort dshort { get; set; }
        public TinkoffFuturesDlongMin dlongMin { get; set; }
        public TinkoffFuturesDshortMin dshortMin { get; set; }
        public bool? shortEnabledFlag { get; set; }
        public string name { get; set; }
        public string exchange { get; set; }
        public Timestamp firstTradeDate { get; set; }
        public Timestamp lastTradeDate { get; set; }
        public string futuresType { get; set; }
        public string assetType { get; set; }
        public string basicAsset { get; set; }
        public TinkoffFuturesBasicAssetSize basicAssetSize { get; set; }
        public string countryOfRisk { get; set; }
        public string countryOfRiskName { get; set; }
        public string sector { get; set; }
        public Timestamp expirationDate { get; set; }
        public string tradingStatus { get; set; }
        public bool? otcFlag { get; set; }
        public bool? buyAvailableFlag { get; set; }
        public bool? sellAvailableFlag { get; set; }
        public TinkoffFuturesMinPriceIncrement minPriceIncrement { get; set; }
        public bool? apiTradeAvailableFlag { get; set; }
        public string uid { get; set; }
        public string realExchange { get; set; }
        public string positionUid { get; set; }
        public string basicAssetPositionUid { get; set; }
        public bool? forIisFlag { get; set; }
        public bool? forQualInvestorFlag { get; set; }
        public bool? weekendFlag { get; set; }
        public bool? blockedTcaFlag { get; set; }
        public Timestamp first1minCandleDate { get; set; }
        public Timestamp first1dayCandleDate { get; set; }
        public DateTime Update { get; set; }
    }

    public class TinkoffFuturesKlong
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffFuturesKshort
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffFuturesMinPriceIncrement
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffFuturesQuery
    {
        public List<TinkoffFuturesInstrument> instruments { get; set; }
    }
}
