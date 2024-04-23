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
            BaseAddress = new Uri(MainSettings.BinanceURIV3)
        };
        private static MongoClient _mongoClient = new MongoClient(Config.MongoClientConnection);
        private static ApplicationContext _db = ApplicationContext.Create(_mongoClient.GetDatabase(Config.MongoDbDatabase));
        public static async Task GetCurrentTickersFromBinance()
        {
            Console.WriteLine(MainSettings.BinanceURIV3);
            BinanceTickers? ticker = await _httpClient.GetFromJsonAsync<BinanceTickers>(MainSettings.BinanceTickerList);
            if (ticker != null)
            {
                foreach (var tickers in ticker.symbols)
                {
                    CryptoBinanceTickers? ticker_find = (from i in _db.CryptoBinanceTickers where i.Ticker == tickers.symbol select i).FirstOrDefault();
                    CryptoTickers? ticker_findc = (from i in _db.CryptoTickers where i.Ticker == tickers.symbol select i).FirstOrDefault();
                    string? is_spot = (from i in tickers.permissions where i == "SPOT" select i).FirstOrDefault();
                    string? is_margin = (from i in tickers.permissions where i == "MARGIN" select i).FirstOrDefault();
                    string? is_leveraged = (from i in tickers.permissions where i == "LEVERAGED" select i).FirstOrDefault();
                    if (ticker_find == null)
                    {
                        CryptoBinanceTickers ocpc = new CryptoBinanceTickers();
                        ocpc._id = ObjectId.GenerateNewId();
                        ocpc.Ticker = tickers.symbol;
                        ocpc.BaseAsset = tickers.baseAsset;
                        ocpc.BaseAssetPrecision = tickers.baseAssetPrecision;
                        ocpc.QuoteAsset = tickers.quoteAsset;
                        ocpc.QuoteAssetPrecision = tickers.quoteAssetPrecision;
                        ocpc.Update = DateTime.UtcNow;
                        ocpc.Source = "Binance";
                        ocpc.IsSpot = 0;
                        ocpc.IsMargin = 0;
                        ocpc.IsLeveraged = 0;
                        
                        if (!string.IsNullOrWhiteSpace(is_spot))
                        {
                            ocpc.IsSpot = 1;
                        }
                        
                        if (!string.IsNullOrWhiteSpace(is_margin))
                        {
                            ocpc.IsMargin = 1;
                        }
                        
                        if (!string.IsNullOrWhiteSpace(is_leveraged))
                        {
                            ocpc.IsLeveraged = 1;
                        }
                        _db.CryptoBinanceTickers.Add(ocpc);
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
                        if (!string.IsNullOrWhiteSpace(is_spot))
                        {
                            ocpc.IsSpot = 1;
                        }
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
