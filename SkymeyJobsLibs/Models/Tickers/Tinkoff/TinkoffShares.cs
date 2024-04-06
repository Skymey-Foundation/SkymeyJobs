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
    public class TinkoffSharesDlong
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffSharesDlongMin
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffSharesDshort
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffSharesDshortMin
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffSharesInstrument
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        [JsonPropertyName("_id")]
        public ObjectId? _id { get; set; }
        public string figi { get; set; }
        public string ticker { get; set; }
        public string classCode { get; set; }
        public string isin { get; set; }
        public int? lot { get; set; }
        public string currency { get; set; }
        public TinkoffSharesKlong klong { get; set; }
        public TinkoffSharesKshort kshort { get; set; }
        public TinkoffSharesDlong dlong { get; set; }
        public TinkoffSharesDshort dshort { get; set; }
        public TinkoffSharesDlongMin dlongMin { get; set; }
        public TinkoffSharesDshortMin dshortMin { get; set; }
        public bool? shortEnabledFlag { get; set; }
        public string name { get; set; }
        public string exchange { get; set; }
        public long? issueSize { get; set; }
        public string countryOfRisk { get; set; }
        public string countryOfRiskName { get; set; }
        public string sector { get; set; }
        public long? issueSizePlan { get; set; }
        public TinkoffSharesNominal nominal { get; set; }
        public string tradingStatus { get; set; }
        public bool? otcFlag { get; set; }
        public bool? buyAvailableFlag { get; set; }
        public bool? sellAvailableFlag { get; set; }
        public bool? divYieldFlag { get; set; }
        public string shareType { get; set; }
        public TinkoffSharesMinPriceIncrement minPriceIncrement { get; set; }
        public bool? apiTradeAvailableFlag { get; set; }
        public string uid { get; set; }
        public string realExchange { get; set; }
        public string positionUid { get; set; }
        public bool? forIisFlag { get; set; }
        public bool? forQualInvestorFlag { get; set; }
        public bool? weekendFlag { get; set; }
        public bool? blockedTcaFlag { get; set; }
        public bool? liquidityFlag { get; set; }
        public Timestamp first1minCandleDate { get; set; }
        public Timestamp first1dayCandleDate { get; set; }
        public Timestamp? ipoDate { get; set; }
        public DateTime Update { get; set; }
    }

    public class TinkoffSharesKlong
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffSharesKshort
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffSharesMinPriceIncrement
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffSharesNominal
    {
        public string currency { get; set; }
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffSharesQuery
    {
        public List<TinkoffSharesInstrument> instruments { get; set; }
    }
}
