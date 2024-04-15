using MongoDB.Bson;
using MongoDB.Driver;
using SkymeyJobsLibs;
using SkymeyJobsLibs.Data;
using SkymeyJobsLibs.Models.ActualPrices;
using SkymeyJobsLibs.Models.ActualPrices.Binance;
using SkymeyJobsLibs.Models.ActualPrices.Okex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkymeyOkexActualPrices.Actions.GetPrices.Okex
{
    public class GetPrices
    {
        private static HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(BinanceAcualPrices.URI_Okex)
        };
        private static MongoClient _mongoClient = new MongoClient(Config.MongoClientConnection);
        private static ApplicationContext _db = ApplicationContext.Create(_mongoClient.GetDatabase(Config.MongoDbDatabase));
        public static async Task GetCurrentPricesFromBinance()
        {
            Console.WriteLine(BinanceAcualPrices.URI_Okex);
            OkexCurrentPricesView? ticker = await _httpClient.GetFromJsonAsync<OkexCurrentPricesView>(BinanceAcualPrices.ActualPrices_Okex);
            if (ticker != null)
            {
                foreach (var tickers in ticker.data)
                {
                    Console.WriteLine(tickers.instId);
                    var ticker_find = (from i in _db.OkexCurrentPricesView where i.Ticker == tickers.instId select i).FirstOrDefault();
                    string ticker_search = tickers.instId.Replace("-SWAP", "").Replace("-", "");
                    var ticker_findc = (from i in _db.CurrentPrices where i.Ticker == ticker_search select i).FirstOrDefault();
                    if (ticker_find == null)
                    {
                        OkexCurrentPrices ocp = new OkexCurrentPrices();
                        ocp._id = ObjectId.GenerateNewId();
                        ocp.Ticker = tickers.instId;
                        ocp.Price = Convert.ToDouble(tickers.markPx);
                        ocp.Update = DateTime.UtcNow;
                        _db.OkexCurrentPricesView.Add(ocp);
                    }
                    else
                    {
                        ticker_find.Price = Convert.ToDouble(tickers.markPx);
                        ticker_find.Update = DateTime.UtcNow;
                        _db.OkexCurrentPricesView.Update(ticker_find);
                    }
                    if (ticker_findc == null)
                    {
                        CurrentPrices ocpc = new CurrentPrices();
                        ocpc._id = ObjectId.GenerateNewId();
                        ocpc.Ticker = ticker_search;
                        ocpc.Price = Convert.ToDouble(tickers.markPx);
                        ocpc.Update = DateTime.UtcNow;
                        _db.CurrentPrices.Add(ocpc);
                    }
                    else
                    {
                        ticker_findc.Price = (ticker_findc.Price + Convert.ToDouble(tickers.markPx)) / 2;
                        ticker_findc.Update = DateTime.UtcNow;
                        _db.CurrentPrices.Update(ticker_findc);
                    }

                }
                Console.WriteLine($"{DateTime.UtcNow} ActualOkexPrices: Complete");
                _db.SaveChanges();
            }
        }

    }
}
