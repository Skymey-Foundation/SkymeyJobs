using MongoDB.Driver;
using SkymeyJobsLibs.Data;
using SkymeyJobsLibs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinkoff.InvestApi;
using Google.Protobuf.WellKnownTypes;
using MongoDB.Bson;
using SkymeyJobsLibs.Models.Tickers.Tinkoff;
using static Google.Rpc.Context.AttributeContext.Types;

namespace SkymeyTinkoffCurrencies.Actions.GetCurrencies.Tinkoff
{
    public class GetCurrencies
    {
        private static InvestApiClient? investApiClient = InvestApiClientFactory.Create(Config.TinkoffAPI);
        private static MongoClient _mongoClient = new MongoClient(Config.MongoClientConnection);
        private static ApplicationContext _db = ApplicationContext.Create(_mongoClient.GetDatabase(Config.MongoDbDatabase));
        public static async Task GetCurrenciesFromTinkoff()
        {
            var response = investApiClient.Instruments.Currencies();
            var ticker_finds = (from i in _db.Currencies select i);
            foreach (var item in response.Instruments)
            {
                Console.WriteLine(item.Ticker);
                var ticker_find = (from i in ticker_finds where i.ticker == item.Ticker && i.figi == item.Figi select i).FirstOrDefault();
                if (ticker_find == null)
                {
                    TinkoffCurrenciesInstrument tci = new TinkoffCurrenciesInstrument();
                    tci._id = ObjectId.GenerateNewId();
                    tci.figi = item.Figi;
                    if (ticker_find != null) tci.figi = "";
                    tci.ticker = item.Ticker;
                    if (tci.ticker == null) tci.ticker = "";
                    tci.classCode = item.ClassCode;
                    if (tci.classCode == null) tci.classCode = "";
                    tci.isin = item.Isin;
                    if (tci.isin == null) tci.isin = "";
                    tci.lot = item.Lot;
                    if (tci.lot == null) tci.lot = 0;
                    tci.currency = item.Currency_;
                    if (tci.currency == null) tci.currency = "";
                    tci.shortEnabledFlag = item.ShortEnabledFlag;
                    if (tci.shortEnabledFlag == null) tci.shortEnabledFlag = false;
                    tci.name = item.Name;
                    if (tci.name == null) tci.name = "";
                    tci.exchange = item.Exchange;
                    if (tci.exchange == null) tci.exchange = "";
                    if (item.Nominal != null)
                    {
                        TinkoffCurrenciesNominal tcn = new TinkoffCurrenciesNominal();
                        tcn.currency = item.Nominal.Currency;
                        tcn.units = item.Nominal.Units;
                        tcn.nano = item.Nominal.Nano;
                    }
                    else
                    {
                        tci.nominal = new TinkoffCurrenciesNominal();
                    }
                    tci.countryOfRisk = item.CountryOfRisk;
                    if (tci.countryOfRisk == null) tci.countryOfRisk = "";
                    tci.countryOfRiskName = item.CountryOfRiskName;
                    if (tci.countryOfRiskName == null) tci.countryOfRiskName = "";
                    tci.tradingStatus = item.TradingStatus.ToString();
                    if (tci.tradingStatus == null) tci.tradingStatus = "";
                    tci.otcFlag = item.OtcFlag;
                    if (tci.otcFlag == null) tci.otcFlag = false;
                    tci.buyAvailableFlag = item.BuyAvailableFlag;
                    if (tci.buyAvailableFlag == null) tci.buyAvailableFlag = false;
                    tci.sellAvailableFlag = item.SellAvailableFlag;
                    if (tci.sellAvailableFlag == null) tci.sellAvailableFlag = false;
                    tci.isoCurrencyName = "";
                    if (tci.isoCurrencyName == null) tci.isoCurrencyName = "";
                    if (item.MinPriceIncrement != null)
                    {
                        TinkoffCurrenciesMinPriceIncrement tcmpi = new TinkoffCurrenciesMinPriceIncrement();
                        tcmpi.units = item.MinPriceIncrement.Units;
                        tcmpi.nano = item.MinPriceIncrement.Nano;
                    }
                    else
                    {
                        tci.minPriceIncrement = new TinkoffCurrenciesMinPriceIncrement();
                    }
                    tci.apiTradeAvailableFlag = item.ApiTradeAvailableFlag;
                    if (tci.apiTradeAvailableFlag == null) tci.apiTradeAvailableFlag = false;
                    tci.uid = item.Uid;
                    if (tci.uid == null) tci.uid = "";
                    tci.realExchange = item.RealExchange.ToString();
                    if (tci.realExchange == null) tci.realExchange = "";
                    tci.positionUid = item.PositionUid;
                    if (tci.positionUid == null) tci.positionUid = "";
                    tci.forIisFlag = item.ForIisFlag;
                    if (tci.forIisFlag == null) tci.forIisFlag = false;
                    tci.forQualInvestorFlag = item.ForQualInvestorFlag;
                    if (tci.forQualInvestorFlag == null) tci.forQualInvestorFlag = false;
                    tci.weekendFlag = item.WeekendFlag;
                    if (tci.weekendFlag == null) tci.weekendFlag = false;
                    tci.blockedTcaFlag = item.BlockedTcaFlag;
                    if (tci.blockedTcaFlag == null) tci.blockedTcaFlag = false;
                    tci.first1minCandleDate = item.First1MinCandleDate;
                    if (tci.first1minCandleDate == null) tci.first1minCandleDate = Timestamp.FromDateTime(DateTime.UtcNow);
                    tci.first1dayCandleDate = item.First1DayCandleDate;
                    if (tci.first1dayCandleDate == null) tci.first1dayCandleDate = Timestamp.FromDateTime(DateTime.UtcNow);
                    if (item.Klong != null)
                    {
                        TinkoffCurrenciesKlong tckl = new TinkoffCurrenciesKlong();
                        tckl.units = item.Klong.Nano;
                        tckl.units = item.Klong.Nano;
                        tci.klong = tckl;
                    }
                    else
                    {
                        tci.klong = new TinkoffCurrenciesKlong();
                    }
                    if (item.Kshort != null)
                    {
                        TinkoffCurrenciesKshort tcks = new TinkoffCurrenciesKshort();
                        tcks.units = item.Kshort.Units;
                        tcks.nano = item.Kshort.Nano;
                        tci.kshort = tcks;
                    }
                    else
                    {
                        tci.kshort = new TinkoffCurrenciesKshort();
                    }
                    if (item.Dlong != null)
                    {
                        TinkoffCurrenciesDlong tcdl = new TinkoffCurrenciesDlong();
                        tcdl.units = item.Dlong.Units;
                        tcdl.nano = item.Dlong.Nano;
                        tci.dlong = tcdl;
                    }
                    else
                    {
                        tci.dlong = new TinkoffCurrenciesDlong();
                    }
                    if (item.Dshort != null)
                    {
                        TinkoffCurrenciesDshort tcds = new TinkoffCurrenciesDshort();
                        tcds.units = item.Dshort.Units;
                        tcds.nano = item.Dshort.Nano;
                        tci.dshort = tcds;
                    }
                    else
                    {
                        tci.dshort = new TinkoffCurrenciesDshort();
                    }
                    if (item.DlongMin != null)
                    {
                        TinkoffCurrenciesDlongMin tcdlm = new TinkoffCurrenciesDlongMin();
                        tcdlm.units = item.DlongMin.Units;
                        tcdlm.nano = item.DlongMin.Nano;
                    }
                    else
                    {
                        tci.dlongMin = new TinkoffCurrenciesDlongMin();
                    }
                    if (item.DshortMin != null)
                    {
                        TinkoffCurrenciesDshortMin tcdsm = new TinkoffCurrenciesDshortMin();
                        tcdsm.units = item.DshortMin.Units;
                        tcdsm.nano = item.DshortMin.Nano;

                    }
                    else
                    {
                        tci.dshortMin = new TinkoffCurrenciesDshortMin();
                    }
                    tci.Update = DateTime.UtcNow;
                    _db.Currencies.Add(tci);
                }
            }
            _db.SaveChanges();
        }
    }
}
