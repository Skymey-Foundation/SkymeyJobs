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
    public class TinkoffCurrenciesDlong
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffCurrenciesDlongMin
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffCurrenciesDshort
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffCurrenciesDshortMin
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffCurrenciesInstrument
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
        public TinkoffCurrenciesNominal nominal { get; set; }
        public string countryOfRisk { get; set; }
        public string countryOfRiskName { get; set; }
        public string tradingStatus { get; set; }
        public bool? otcFlag { get; set; }
        public bool? buyAvailableFlag { get; set; }
        public bool? sellAvailableFlag { get; set; }
        public string isoCurrencyName { get; set; }
        public TinkoffCurrenciesMinPriceIncrement minPriceIncrement { get; set; }
        public bool? apiTradeAvailableFlag { get; set; }
        public string uid { get; set; }
        public string realExchange { get; set; }
        public string positionUid { get; set; }
        public bool? forIisFlag { get; set; }
        public bool? forQualInvestorFlag { get; set; }
        public bool? weekendFlag { get; set; }
        public bool? blockedTcaFlag { get; set; }
        public Timestamp first1minCandleDate { get; set; }
        public Timestamp first1dayCandleDate { get; set; }
        public TinkoffCurrenciesKlong klong { get; set; }
        public TinkoffCurrenciesKshort kshort { get; set; }
        public TinkoffCurrenciesDlong dlong { get; set; }
        public TinkoffCurrenciesDshort dshort { get; set; }
        public TinkoffCurrenciesDlongMin dlongMin { get; set; }
        public TinkoffCurrenciesDshortMin dshortMin { get; set; }
        public DateTime Update { get; set; }
    }

    public class TinkoffCurrenciesKlong
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffCurrenciesKshort
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffCurrenciesMinPriceIncrement
    {
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffCurrenciesNominal
    {
        public string currency { get; set; }
        public long units { get; set; }
        public int nano { get; set; }
    }

    public class TinkoffCurrenciesQuery
    {
        public List<TinkoffCurrenciesInstrument> instruments { get; set; }
    }
}
