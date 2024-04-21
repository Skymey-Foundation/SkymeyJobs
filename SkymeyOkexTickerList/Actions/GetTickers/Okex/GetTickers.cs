using MongoDB.Bson;
using MongoDB.Driver;
using SkymeyJobsLibs.Data;
using SkymeyJobsLibs.Models.ActualPrices.Binance;
using SkymeyJobsLibs.Models.ActualPrices;
using SkymeyJobsLibs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SkymeyJobsLibs.Models.Tickers.Crypto.Binance;
using SkymeyJobsLibs.Models.Tickers.Crypto;
using SkymeyJobsLibs.Models.Tickers.Crypto.Okex;

namespace SkymeyOkexTickerList.Actions.GetTickers.Okex
{
    public class GetTickers
    {
        private static HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(BinanceAcualPrices.OkexTickerListURI)
        };
        private static MongoClient _mongoClient = new MongoClient(Config.MongoClientConnection);
        private static ApplicationContext _db = ApplicationContext.Create(_mongoClient.GetDatabase(Config.MongoDbDatabase));
        public static async Task GetCurrentTickersFromOkexSPOT()
        {
            Console.WriteLine(BinanceAcualPrices.OkexTickerListURI);
            OkexTickers? ticker = await _httpClient.GetFromJsonAsync<OkexTickers>(BinanceAcualPrices.OkexTickerListSPOT);
            if (ticker != null)
            {
                List<CryptoOkexTickers>? ticker_find2 = (from i in _db.CryptoOkexTickers select i).ToList();
                var ticker_findc2 = (from i in _db.CryptoTickers select i).ToList();
                int? max_value = (from i in ticker_findc2 orderby i.Id descending select i.Id).FirstOrDefault();
                if (max_value == null)
                {
                    max_value = 1;
                }
                foreach (var tickers in ticker.data)
                {
                    string ticker_okex = tickers.instId.ToString().Replace("-","");
                    CryptoOkexTickers? ticker_find = (from i in ticker_find2 where i.Ticker == ticker_okex select i).FirstOrDefault();
                    CryptoTickers? ticker_findc = (from i in ticker_findc2 where i.Ticker == ticker_okex select i).FirstOrDefault();
                    if (ticker_find == null)
                    {
                        CryptoOkexTickers ocpc = new CryptoOkexTickers();
                        ocpc._id = ObjectId.GenerateNewId();
                        ocpc.Ticker = ticker_okex;
                        ocpc.BaseAsset = tickers.baseCcy;
                        ocpc.BaseAssetPrecision = tickers.maxLmtSz.Length;
                        ocpc.QuoteAsset = tickers.quoteCcy;
                        ocpc.QuoteAssetPrecision = tickers.maxLmtSz.Length;
                        ocpc.Update = DateTime.UtcNow;
                        ocpc.Source = "Okex";
                        ocpc.IsSpot = 1;
                        ocpc.IsMargin = 0;
                        ocpc.IsLeveraged = 0;

                        _db.CryptoOkexTickers.Add(ocpc);
                    }
                    if (ticker_findc == null)
                    {
                        CryptoTickers ocpc = new CryptoTickers();
                        ocpc._id = ObjectId.GenerateNewId();
                        ocpc.Id = max_value;
                        max_value++;
                        ocpc.Ticker = ticker_okex;
                        ocpc.BaseAsset = tickers.baseCcy;
                        ocpc.BaseAssetPrecision = tickers.maxLmtSz.Length;
                        ocpc.QuoteAsset = tickers.quoteCcy;
                        ocpc.QuoteAssetPrecision = tickers.maxLmtSz.Length;
                        ocpc.IsSpot= 1;
                        ocpc.Update = DateTime.UtcNow;
                        _db.CryptoTickers.Add(ocpc);
                    }
                }
                Console.WriteLine($"{DateTime.UtcNow} ActualOkexTickers SPOT: Complete");
                _db.SaveChanges();
            }
        }
        public static async Task GetCurrentTickersFromOkexFUTURES()
        {
            Console.WriteLine(BinanceAcualPrices.OkexTickerListURI);
            OkexTickers? ticker = await _httpClient.GetFromJsonAsync<OkexTickers>(BinanceAcualPrices.OkexTickerListFUTURES);
            if (ticker != null)
            {
                List<CryptoOkexTickers>? ticker_find2 = (from i in _db.CryptoOkexTickers select i).ToList();
                List<CryptoTickers>? ticker_findc2 = (from i in _db.CryptoTickers select i).ToList();
                int? max_value = (from i in ticker_findc2 orderby i.Id descending select i.Id).FirstOrDefault();
                if (max_value == null)
                {
                    max_value = 1;
                }
                foreach (var tickers in ticker.data)
                {
                    string ticker_okex = tickers.instId.ToString().Replace("-", "");
                    CryptoOkexTickers? ticker_find = (from i in ticker_find2 where i.Ticker == ticker_okex select i).FirstOrDefault();
                    CryptoTickers? ticker_findc = (from i in ticker_findc2 where i.Ticker == ticker_okex select i).FirstOrDefault();
                    if (ticker_find == null)
                    {
                        CryptoOkexTickers ocpc = new CryptoOkexTickers();
                        ocpc._id = ObjectId.GenerateNewId();
                        ocpc.Ticker = ticker_okex;
                        ocpc.BaseAsset = tickers.baseCcy;
                        ocpc.BaseAssetPrecision = tickers.maxLmtSz.Length;
                        ocpc.QuoteAsset = tickers.quoteCcy;
                        ocpc.QuoteAssetPrecision = tickers.maxLmtSz.Length;
                        ocpc.Update = DateTime.UtcNow;
                        ocpc.Source = "Okex";
                        ocpc.IsSpot = 0;
                        ocpc.IsMargin = 0;
                        ocpc.IsLeveraged = 1;

                        _db.CryptoOkexTickers.Add(ocpc);
                    }
                    else
                    {
                        ticker_find.IsLeveraged = 1;
                        _db.Update(ticker_find);
                    }
                    if (ticker_findc == null)
                    {
                        CryptoTickers ocpc = new CryptoTickers();
                        ocpc._id = ObjectId.GenerateNewId(); 
                        ocpc.Id = max_value;
                        max_value++;
                        ocpc.Ticker = ticker_okex;
                        ocpc.BaseAsset = tickers.baseCcy;
                        ocpc.BaseAssetPrecision = tickers.maxLmtSz.Length;
                        ocpc.QuoteAsset = tickers.quoteCcy;
                        ocpc.QuoteAssetPrecision = tickers.maxLmtSz.Length;
                        ocpc.Update = DateTime.UtcNow;
                        _db.CryptoTickers.Add(ocpc);
                    }
                }
                Console.WriteLine($"{DateTime.UtcNow} ActualOkexTickers LEVERAGE: Complete");
                _db.SaveChanges();
            }
        }
        public static async Task GetCurrentTickersFromOkexMARGIN()
        {
            Console.WriteLine(BinanceAcualPrices.OkexTickerListURI);
            OkexTickers? ticker = await _httpClient.GetFromJsonAsync<OkexTickers>(BinanceAcualPrices.OkexTickerListMARGIN);
            if (ticker != null)
            {
                List<CryptoOkexTickers>? ticker_find2 = (from i in _db.CryptoOkexTickers select i).ToList();
                List<CryptoTickers>? ticker_findc2 = (from i in _db.CryptoTickers select i).ToList();
                int? max_value = (from i in ticker_findc2 orderby i.Id descending select i.Id).FirstOrDefault();
                if (max_value == null)
                {
                    max_value = 1;
                }
                foreach (var tickers in ticker.data)
                {
                    string ticker_okex = tickers.instId.ToString().Replace("-", "");
                    CryptoOkexTickers? ticker_find = (from i in ticker_find2 where i.Ticker == ticker_okex select i).FirstOrDefault();
                    CryptoTickers? ticker_findc = (from i in ticker_findc2 where i.Ticker == ticker_okex select i).FirstOrDefault();
                    if (ticker_find == null)
                    {
                        CryptoOkexTickers ocpc = new CryptoOkexTickers();
                        ocpc._id = ObjectId.GenerateNewId();
                        ocpc.Ticker = ticker_okex;
                        ocpc.BaseAsset = tickers.baseCcy;
                        ocpc.BaseAssetPrecision = tickers.maxLmtSz.Length;
                        ocpc.QuoteAsset = tickers.quoteCcy;
                        ocpc.QuoteAssetPrecision = tickers.maxLmtSz.Length;
                        ocpc.Update = DateTime.UtcNow;
                        ocpc.Source = "Okex";
                        ocpc.IsSpot = 0;
                        ocpc.IsMargin = 1; 
                        ocpc.IsLeveraged = 0;

                        _db.CryptoOkexTickers.Add(ocpc);
                    }
                    else
                    {
                        ticker_find.IsMargin = 1;
                        _db.Update(ticker_find);
                    }
                    if (ticker_findc == null)
                    {
                        CryptoTickers ocpc = new CryptoTickers();
                        ocpc._id = ObjectId.GenerateNewId();
                        ocpc.Id = max_value;
                        max_value++;
                        ocpc.Ticker = ticker_okex;
                        ocpc.BaseAsset = tickers.baseCcy;
                        ocpc.BaseAssetPrecision = tickers.maxLmtSz.Length;
                        ocpc.QuoteAsset = tickers.quoteCcy;
                        ocpc.QuoteAssetPrecision = tickers.maxLmtSz.Length;
                        ocpc.Update = DateTime.UtcNow;
                        _db.CryptoTickers.Add(ocpc);
                    }
                }
                Console.WriteLine($"{DateTime.UtcNow} ActualOkexTickers MARGIN: Complete");
                _db.SaveChanges();
            }
        }
    }
}
