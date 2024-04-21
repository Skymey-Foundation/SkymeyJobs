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
using SkymeyJobsLibs.Models.Tickers.Crypto.CryptoInstruments;

namespace SkymeyOkexTickerList.Actions.GetTickers.Okex
{
    public class GetInstruments
    {
        private static HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(MainSettings.CMC_URI)
        };
        private static MongoClient _mongoClient = new MongoClient(Config.MongoClientConnection);
        private static ApplicationContext _db = ApplicationContext.Create(_mongoClient.GetDatabase(Config.MongoDbDatabase));
        public static async Task GetCurrentInstrumentsFromCMC()
        {
            try
            {
                Console.WriteLine(MainSettings.CMC_URI);
                _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", MainSettings.CMC_API);
                CryptoInstruments? ticker = await _httpClient.GetFromJsonAsync<CryptoInstruments>(MainSettings.CMC_MAP);
                if (ticker != null)
                {
                    List<CryptoInstrumentsDB>? ticker_find2 = (from i in _db.CryptoInstrumentsDB select i).ToList();
                    int? max_value = (from i in ticker_find2 orderby i.Id descending select i.Id).FirstOrDefault();
                    if (max_value == null)
                    {
                        max_value = 1;
                    }
                    foreach (var tickers in ticker.data)
                    {
                        CryptoInstrumentsDB? ticker_findc = (from i in ticker_find2 where i.Symbol == tickers.symbol select i).FirstOrDefault();
                        if (ticker_findc == null)
                        {
                            CryptoInstrumentsDB ocpc = new CryptoInstrumentsDB();
                            ocpc._id = ObjectId.GenerateNewId();
                            ocpc.Id = max_value;
                            max_value++;
                            ocpc.Rank = 99999;
                            ocpc.Name = tickers.name;
                            ocpc.Symbol = tickers.symbol;
                            ocpc.Slug = tickers.slug;
                            ocpc.Is_active = tickers.is_active;
                            if (tickers.platform != null)
                            {
                                ocpc.Platform = new PlatformDB { Name = tickers.platform.name, Slug = tickers.platform.slug, Symbol = tickers.platform.symbol, Token_address = tickers.platform.token_address };
                            }
                            else
                            {
                                ocpc.Platform = new PlatformDB() { };
                            }
                            ocpc.Update = DateTime.UtcNow;
                            _db.CryptoInstrumentsDB.Add(ocpc);
                        }
                        else
                        {
                            ticker_findc.Is_active = tickers.is_active;
                            if (tickers.platform != null)
                            {
                                ticker_findc.Platform = new PlatformDB { Name = tickers.platform.name, Slug = tickers.platform.slug, Symbol = tickers.platform.symbol, Token_address = tickers.platform.token_address };
                            }
                            else
                            {
                                ticker_findc.Platform = new PlatformDB() { };
                            }
                            ticker_findc.Update = DateTime.UtcNow;
                            _db.CryptoInstrumentsDB.Update(ticker_findc);
                        }
                    }
                    Console.WriteLine($"{DateTime.UtcNow} ActualCMCInstruments SPOT: Complete");
                    _db.SaveChanges();
                }
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
            finally { }
        }
    }
}
