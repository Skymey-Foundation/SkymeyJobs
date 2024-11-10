using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using SkymeyJobsLibs;
using SkymeyJobsLibs.Data;
using SkymeyJobsLibs.Models.ActualPrices;
using SkymeyJobsLibs.Models.ActualPrices.Binance;
using SkymeyJobsLibs.Models.ActualPrices.Okex;
using SkymeyJobsLibs.Models.Crypto.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkymeyOkexActualPrices.Actions.GetTokens.Crypto
{
    public class GetTokens
    {
        private static HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(MainSettings.URI_Okex)
        };
        private static MongoClient _mongoClient = new MongoClient(Config.MongoClientConnection);
        private ApplicationContext _db = ApplicationContext.Create(_mongoClient.GetDatabase(Config.MongoDbDatabase));
        public async Task ActualDataTokens()
        {
            Console.WriteLine(MainSettings.URI_Okex);
            List<SkymeyJobsLibs.Models.Crypto.Tokens.API_TOKEN> tokens = (from i in _db.API_TOKEN select i).ToList();
            List<SkymeyJobsLibs.Models.Crypto.Tokens.CurrentPrices> prices = (from i in _db.CurrentPrices select i).ToList();
            TokenListViewModel tokensList = new TokenListViewModel { CurrentPricess = prices, TokenLists = tokens };
            var resp = (from i in tokensList.TokenLists
                        join ic in tokensList.CurrentPricess on i.Symbol + "USDT" equals ic.Ticker
                        select new TokenList
                        {
                            Symbol = i.Symbol+"USDT",
                            Name = i.Name,
                            Price = ic.Price.ToString(),
                            tfhc = "0",
                            sdc = "0"
                        }).ToList();
            if (resp != null)
            {
                foreach (var tickers in resp)
                {
                    Console.WriteLine(tickers.Symbol);
                    var ticker_findc = (from i in _db.crypto_index_page_tokens where i.Symbol == tickers.Symbol select i).FirstOrDefault();
                    if (ticker_findc == null)
                    {
                        Console.WriteLine(tickers.Symbol + " Create");
                        TokenList ocpc = new TokenList();
                        ocpc._id = ObjectId.GenerateNewId();
                        ocpc.Symbol = tickers.Symbol;
                        ocpc.Name = tickers.Name;
                        ocpc.Price = tickers.Price;
                        ocpc.tfhc = tickers.tfhc;
                        ocpc.sdc = tickers.sdc;
                        _db.crypto_index_page_tokens.Add(ocpc);
                    }
                    else
                    {
                        Console.WriteLine($"{tickers.Symbol} Update, Price: {tickers.Price}");
                        ticker_findc.Price = tickers.Price;
                        _db.crypto_index_page_tokens.Update(ticker_findc);
                    }

                }
                Console.WriteLine($"{DateTime.UtcNow} Actual index page tokens: Complete");
                await _db.SaveChangesAsync();
            }
        }

    }
}
