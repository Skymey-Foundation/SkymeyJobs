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

namespace SkymeyTinkoffBondList.Actions.GetBonds
{
    public class GetBonds
    {
        private static InvestApiClient? investApiClient = InvestApiClientFactory.Create(Config.TinkoffAPI);
        private static MongoClient _mongoClient = new MongoClient(Config.MongoClientConnection);
        private static ApplicationContext _db = ApplicationContext.Create(_mongoClient.GetDatabase(Config.MongoDbDatabase));
        public static async Task GetBondsFromTinkoff()
        {
            var response = investApiClient.Instruments.Bonds();
            var ticker_finds = (from i in _db.Bonds select i);
            foreach (var item in response.Instruments)
            {
                var ticker_find = (from i in ticker_finds where i.ticker == item.Ticker && i.figi == item.Figi select i).FirstOrDefault();
                if (ticker_find == null)
                {
                    TinkoffBondInstrument tbi = new TinkoffBondInstrument();
                    tbi._id = ObjectId.GenerateNewId();
                    tbi.figi = item.Figi;
                    if (tbi.figi == null) tbi.figi = "";
                    tbi.ticker = item.Ticker;
                    if (tbi.ticker == null) tbi.ticker = "";
                    tbi.classCode = item.ClassCode;
                    if (tbi.classCode == null) tbi.classCode = "";
                    tbi.isin = item.Isin;
                    if (tbi.isin == null) tbi.isin = "";
                    tbi.lot = item.Lot;
                    if (tbi.lot == null) tbi.lot = 0;
                    tbi.currency = item.Currency;
                    if (tbi.currency == null) tbi.currency = "";
                    tbi.shortEnabledFlag = item.ShortEnabledFlag;
                    if (tbi.shortEnabledFlag == null) tbi.shortEnabledFlag = false;
                    tbi.name = item.Name;
                    if (tbi.name == null) tbi.name = "";
                    tbi.exchange = item.Exchange;
                    if (tbi.exchange == null) tbi.exchange = "";
                    tbi.couponQuantityPerYear = item.CouponQuantityPerYear;
                    if (tbi.couponQuantityPerYear == null) tbi.couponQuantityPerYear = 0;
                    tbi.maturityDate = item.MaturityDate;
                    if (tbi.maturityDate == null) tbi.maturityDate = Timestamp.FromDateTime(DateTime.UtcNow);
                    TinkoffBondNominal tbn = new TinkoffBondNominal();
                    tbn.currency = item.Nominal.Currency;
                    tbn.units = item.Nominal.Units;
                    tbn.nano = item.Nominal.Nano;
                    tbi.nominal = tbn;
                    if (tbi.nominal == null) tbi.nominal = new TinkoffBondNominal();
                    TinkoffBondInitialNominal tbin = new TinkoffBondInitialNominal();
                    tbin.currency = item.InitialNominal.Currency;
                    tbin.units = item.InitialNominal.Units;
                    tbin.nano = item.InitialNominal.Nano;
                    tbi.initialNominal = tbin;
                    if (tbi.initialNominal == null) tbi.initialNominal = new TinkoffBondInitialNominal();
                    tbi.stateRegDate = item.StateRegDate;
                    if (tbi.stateRegDate == null) tbi.stateRegDate = Timestamp.FromDateTime(DateTime.UtcNow);
                    tbi.placementDate = item.PlacementDate;
                    if (tbi.placementDate == null) tbi.placementDate = Timestamp.FromDateTime(DateTime.UtcNow);
                    TinkoffBondAciValue tbav = new TinkoffBondAciValue();
                    tbav.currency = item.AciValue.Currency;
                    tbav.units = item.AciValue.Units;
                    tbav.nano = item.AciValue.Nano;
                    tbi.aciValue = tbav;
                    if (tbi.aciValue == null) tbi.aciValue = new TinkoffBondAciValue();
                    tbi.countryOfRisk = item.CountryOfRisk;
                    if (tbi.countryOfRisk == null) tbi.countryOfRisk = "";
                    tbi.countryOfRiskName = item.CountryOfRiskName;
                    if (tbi.countryOfRiskName == null) tbi.countryOfRiskName = "";
                    tbi.sector = item.Sector;
                    if (tbi.sector == null) tbi.sector = "";
                    tbi.issueKind = item.IssueKind;
                    if (tbi.issueKind == null) tbi.issueKind = "";
                    tbi.issueSize = item.IssueSize;
                    if (tbi.issueSize == null) tbi.issueSize = 0;
                    tbi.issueSizePlan = item.IssueSizePlan;
                    if (tbi.issueSizePlan == null) tbi.issueSizePlan = 0;
                    tbi.tradingStatus = item.TradingStatus.ToString();
                    if (tbi.tradingStatus == null) tbi.tradingStatus = "";
                    tbi.otcFlag = item.OtcFlag;
                    if (tbi.otcFlag == null) tbi.otcFlag = false;
                    tbi.buyAvailableFlag = item.BuyAvailableFlag;
                    if (tbi.buyAvailableFlag == null) tbi.buyAvailableFlag = false;
                    tbi.sellAvailableFlag = item.SellAvailableFlag;
                    if (tbi.sellAvailableFlag == null) tbi.sellAvailableFlag = false;
                    tbi.floatingCouponFlag = item.FloatingCouponFlag;
                    if (tbi.floatingCouponFlag == null) tbi.floatingCouponFlag = false;
                    tbi.perpetualFlag = item.PerpetualFlag;
                    if (tbi.perpetualFlag == null) tbi.perpetualFlag = false;
                    tbi.amortizationFlag = item.AmortizationFlag;
                    if (tbi.amortizationFlag == null) tbi.amortizationFlag = false;
                    TinkoffBondMinPriceIncrement tbmpi = new TinkoffBondMinPriceIncrement();
                    if (item.MinPriceIncrement != null)
                    {
                        tbmpi.units = item.MinPriceIncrement.Units;
                        tbmpi.nano = item.MinPriceIncrement.Nano;
                        tbi.minPriceIncrement = tbmpi;
                    }
                    if (tbi.minPriceIncrement == null) tbi.minPriceIncrement = new TinkoffBondMinPriceIncrement();
                    tbi.apiTradeAvailableFlag = item.ApiTradeAvailableFlag;
                    if (tbi.apiTradeAvailableFlag == null) tbi.apiTradeAvailableFlag = false;
                    tbi.uid = item.Uid;
                    if (tbi.uid == null) tbi.uid = "";
                    tbi.realExchange = item.RealExchange.ToString();
                    if (tbi.realExchange == null) tbi.realExchange = "";
                    tbi.positionUid = item.PositionUid;
                    if (tbi.positionUid == null) tbi.positionUid = "";
                    tbi.forIisFlag = item.ForIisFlag;
                    if (tbi.forIisFlag == null) tbi.forIisFlag = false;
                    tbi.forQualInvestorFlag = item.ForQualInvestorFlag;
                    if (tbi.forQualInvestorFlag == null) tbi.forQualInvestorFlag = false;
                    tbi.weekendFlag = item.WeekendFlag;
                    if (tbi.weekendFlag == null) tbi.weekendFlag = false;
                    tbi.blockedTcaFlag = item.BlockedTcaFlag;
                    if (tbi.blockedTcaFlag == null) tbi.blockedTcaFlag = false;
                    tbi.subordinatedFlag = item.SubordinatedFlag;
                    if (tbi.subordinatedFlag == null) tbi.subordinatedFlag = false;
                    tbi.liquidityFlag = item.LiquidityFlag;
                    if (tbi.liquidityFlag == null) tbi.liquidityFlag = false;
                    tbi.first1dayCandleDate = item.First1DayCandleDate;
                    if (tbi.first1dayCandleDate == null) tbi.first1dayCandleDate = Timestamp.FromDateTime(DateTime.UtcNow);
                    tbi.first1minCandleDate = item.First1MinCandleDate;
                    if (tbi.first1minCandleDate == null) tbi.first1minCandleDate = Timestamp.FromDateTime(DateTime.UtcNow);
                    tbi.riskLevel = item.RiskLevel.ToString();
                    if (tbi.riskLevel == null) tbi.riskLevel = "";
                    TinkoffBondPlacementPrice tbpp = new TinkoffBondPlacementPrice();
                    tbpp.currency = item.PlacementPrice.Currency;
                    if (tbi.currency == null) tbi.currency = "";
                    tbpp.units = item.PlacementPrice.Units;
                    tbpp.currency = item.PlacementPrice.Currency;
                    tbi.placementPrice = tbpp;
                    tbi.Update = DateTime.UtcNow;
                    _db.Bonds.Add(tbi);
                }
            }
            Console.WriteLine($"{DateTime.UtcNow} TinkoffBonds: Complete");
            _db.SaveChanges();
        }
    }
}
