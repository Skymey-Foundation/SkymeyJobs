using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.IdGenerators;
using Google.Protobuf.WellKnownTypes;

namespace SkymeyJobsLibs.Models.Tickers.Tinkoff
{
    public class TinkoffBondAciValue
    {
        public string currency { get; set; }
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffBondInitialNominal
    {
        public string currency { get; set; }
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffBondInstrument
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
        public bool? shortEnabledFlag { get; set; }
        public string name { get; set; }
        public string exchange { get; set; }
        public int? couponQuantityPerYear { get; set; }
        public Timestamp maturityDate { get; set; }
        public TinkoffBondNominal nominal { get; set; }
        public TinkoffBondInitialNominal initialNominal { get; set; }
        public Timestamp stateRegDate { get; set; }
        public Timestamp placementDate { get; set; }
        public TinkoffBondPlacementPrice placementPrice { get; set; }
        public TinkoffBondAciValue aciValue { get; set; }
        public string countryOfRisk { get; set; }
        public string countryOfRiskName { get; set; }
        public string sector { get; set; }
        public string issueKind { get; set; }
        public long? issueSize { get; set; }
        public long? issueSizePlan { get; set; }
        public string tradingStatus { get; set; }
        public bool? otcFlag { get; set; }
        public bool? buyAvailableFlag { get; set; }
        public bool? sellAvailableFlag { get; set; }
        public bool? floatingCouponFlag { get; set; }
        public bool? perpetualFlag { get; set; }
        public bool? amortizationFlag { get; set; }
        public TinkoffBondMinPriceIncrement minPriceIncrement { get; set; }
        public bool? apiTradeAvailableFlag { get; set; }
        public string uid { get; set; }
        public string realExchange { get; set; }
        public string positionUid { get; set; }
        public bool? forIisFlag { get; set; }
        public bool? forQualInvestorFlag { get; set; }
        public bool? weekendFlag { get; set; }
        public bool? blockedTcaFlag { get; set; }
        public bool? subordinatedFlag { get; set; }
        public bool? liquidityFlag { get; set; }
        public Timestamp first1minCandleDate { get; set; }
        public Timestamp first1dayCandleDate { get; set; }
        public string riskLevel { get; set; }
        public DateTime Update { get; set; }
    }

    public class TinkoffBondMinPriceIncrement
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffBondNominal
    {
        public string currency { get; set; }
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffBondPlacementPrice
    {
        public string currency { get; set; }
        public long? units { get; set; }
        public int? nano { get; set; }
    }

    public class TinkoffBondQuery
    {
        public List<TinkoffBondInstrument> instruments { get; set; }
    }
}
