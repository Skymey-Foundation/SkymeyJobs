﻿using MongoDB.Bson;
using MongoDB.Driver;
using SkymeyJobsLibs;
using SkymeyJobsLibs.Data;
using SkymeyJobsLibs.Models.ActualPrices;
using SkymeyJobsLibs.Models.ActualPrices.Binance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkymeyBinanceActualPrices.Actions.GetPrices.Binance
{
    public class GetPrices
    {
        private static HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(MainSettings.URI)
        };
        private static MongoClient _mongoClient = new MongoClient(Config.MongoClientConnection);
        private static ApplicationContext _db = ApplicationContext.Create(_mongoClient.GetDatabase(Config.MongoDbDatabase));
        public static async Task GetCurrentPricesFromBinance()
        {
            Console.WriteLine(MainSettings.URI);
            List<BinanceCurrentPrice>? ticker = await _httpClient.GetFromJsonAsync<List<BinanceCurrentPrice>>(MainSettings.ActualPrices);
            if (ticker != null)
            {
                foreach (var tickers in ticker)
                {
                    BinanceCurrentPrice? ticker_find = (from i in _db.BinanceCurrentPrices where i.symbol == tickers.symbol select i).FirstOrDefault();
                    SkymeyJobsLibs.Models.Crypto.Tokens.CurrentPrices? ticker_findc = (from i in _db.CurrentPrices where i.Ticker == tickers.symbol select i).FirstOrDefault();
                    if (ticker_find == null)
                    {
                        BinanceCurrentPrice ocp = new BinanceCurrentPrice();
                        ocp._id = ObjectId.GenerateNewId();
                        ocp.symbol = tickers.symbol;
                        ocp.price = tickers.price;
                        ocp.Update = DateTime.UtcNow;
                        _db.BinanceCurrentPrices.Add(ocp);
                    }
                    else
                    {
                        ticker_find.price = tickers.price;
                        ticker_find.Update = DateTime.UtcNow;
                        _db.BinanceCurrentPrices.Update(ticker_find);
                    }
                    if (ticker_findc == null)
                    {
                        SkymeyJobsLibs.Models.Crypto.Tokens.CurrentPrices ocpc = new SkymeyJobsLibs.Models.Crypto.Tokens.CurrentPrices();
                        ocpc._id = ObjectId.GenerateNewId();
                        ocpc.Ticker = tickers.symbol;
                        ocpc.Price = Convert.ToDouble(tickers.price.Replace(".", ","));
                        ocpc.Update = DateTime.UtcNow;
                        _db.CurrentPrices.Add(ocpc);
                    }
                    else
                    {
                        ticker_findc.Price = (ticker_findc.Price + Convert.ToDouble(tickers.price.Replace(".", ","))) / 2;
                        ticker_findc.Update = DateTime.UtcNow;
                        _db.CurrentPrices.Update(ticker_findc);
                    }
                }
                Console.WriteLine($"{DateTime.UtcNow} ActualBinancePrices: Complete");
                _db.SaveChanges();
            }
        }

    }
}
