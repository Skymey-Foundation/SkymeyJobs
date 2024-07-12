using MongoDB.Driver;
using SkymeyJobsLibs.Data;
using SkymeyJobsLibs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkymeyBlockchainEthereum.Models;
using System.Net.Http.Json;
using Google.Protobuf.Compiler;
using System.Text.Json;
using MongoDB.Bson;
using System.Numerics;
using SkymeyJobsLibs.Models.Tickers.Crypto.CryptoInstruments;

namespace SkymeyBlockchainEthereum.Actions.UpdateInstruments.Ethereum
{
    public class UpdateInstruments
    {
        private static HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(MainSettings.Etherscan)
        };
        private static MongoClient _mongoClient = new MongoClient(Config.MongoClientConnection);
        private static ApplicationContext _db = ApplicationContext.Create(_mongoClient.GetDatabase(Config.MongoDbDatabase));
        public static async Task Update()
        {
            var current_instruments = (from i in _db.CryptoInstrumentsDB select i).ToList();
            PlatformDB? platforms = new PlatformDB() { Name = "Ethereum", Slug = "ethereum", Symbol = "ETH", Token_address = "0" };
            //current_instruments = current_instruments.Where(x => current_instruments.Select(x => x.Platform).Contains(platforms.FirstOrDefault()));
            current_instruments = (from i in current_instruments where i.Platform.Name == platforms.Name select i).ToList();
            foreach (var item in current_instruments)
            {
                try
                {
                    var tokenSupply = await _httpClient.GetFromJsonAsync<TokenSupply>(MainSettings.Etherscan + "contractaddress=" + item.Platform.Token_address + "&action=tokensupply&module=stats");
                    BigInteger supply = BigInteger.Parse(tokenSupply.result) / 1000000000000000000;
                    item.MaxSupply = supply.ToString();
                    item.Update = DateTime.UtcNow;
                    _db.CryptoInstrumentsDB.Update(item);
                    Console.WriteLine("Worked with " + item.Name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            _db.SaveChanges();
        }
    }
}
