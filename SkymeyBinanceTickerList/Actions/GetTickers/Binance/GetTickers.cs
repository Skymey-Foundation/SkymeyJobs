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

namespace SkymeyBinanceTickerList.Actions.GetTickers.Binance
{
    public class GetTickers
    {
        private static HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(BinanceAcualPrices.BinanceURIV3)
        };
        private static MongoClient _mongoClient = new MongoClient(Config.MongoClientConnection);
        private static ApplicationContext _db = ApplicationContext.Create(_mongoClient.GetDatabase(Config.MongoDbDatabase));
        public static async Task GetCurrentTickersFromBinance()
        {
            Console.WriteLine(BinanceAcualPrices.BinanceURIV3);
            BinanceTickers? ticker = await _httpClient.GetFromJsonAsync<BinanceTickers>(BinanceAcualPrices.BinanceTickerList);
            if (ticker != null)
            {
                foreach (var tickers in ticker.symbols)
                {
                    Symbol? ticker_find = (from i in _db.BinanceTickers where i.symbol == tickers.symbol select i).FirstOrDefault();
                    CryptoTickers? ticker_findc = (from i in _db.CryptoTickers where i.Ticker == tickers.symbol select i).FirstOrDefault();
                    if (ticker_find == null)
                    {
                        Symbol ocp = new Symbol();
                        ocp._id = ObjectId.GenerateNewId();
                        ocp.symbol = tickers.symbol;
                        ocp.baseAsset = tickers.baseAsset;
                        ocp.baseAssetPrecision = tickers.baseAssetPrecision;
                        ocp.quoteAsset = tickers.quoteAsset;
                        ocp.quoteAssetPrecision = tickers.quoteAssetPrecision;
                        ocp.Update = DateTime.UtcNow;
                        _db.BinanceTickers.Add(ocp);
                    }
                    if (ticker_findc == null)
                    {
                        CryptoTickers ocpc = new CryptoTickers();
                        ocpc._id = ObjectId.GenerateNewId();
                        ocpc.Ticker = tickers.symbol;
                        ocpc.BaseAsset = tickers.baseAsset;
                        ocpc.BaseAssetPrecision = tickers.baseAssetPrecision;
                        ocpc.QuoteAsset = tickers.quoteAsset;
                        ocpc.QuoteAssetPrecision = tickers.quoteAssetPrecision;
                        ocpc.Update = DateTime.UtcNow;
                        _db.CryptoTickers.Add(ocpc);
                    }
                }
                Console.WriteLine($"{DateTime.UtcNow} ActualBinanceTickers: Complete");
                _db.SaveChanges();
            }
        }
    }
}
