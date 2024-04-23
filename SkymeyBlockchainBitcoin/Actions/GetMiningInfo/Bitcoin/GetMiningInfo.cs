using Amazon.Runtime.Internal;
using Google.Protobuf.Compiler;
using MongoDB.Bson;
using MongoDB.Driver;
using RestSharp;
using SkymeyJobsLibs;
using SkymeyJobsLibs.Data;
using SkymeyJobsLibs.Models.ActualPrices;
using SkymeyJobsLibs.Models.ActualPrices.Binance;
using SkymeyJobsLibs.Models.Blockchain.Bitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkymeyBlockchainBitcoin.Actions.GetMiningInfo
{
    public class GetMiningInfo
    {
        private static HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(MainSettings.Bitcoin_URI)
        };
        private static MongoClient _mongoClient = new MongoClient(Config.MongoClientConnection);
        private static ApplicationContext _db = ApplicationContext.Create(_mongoClient.GetDatabase(Config.MongoDbDatabase));
        public static async Task GetMiningDetails()
        {
            var options = new RestClientOptions(MainSettings.Bitcoin_URI)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic " + MainSettings.BitcoinAuth);
            var body = @"{" + "\n" +
            @"    ""method"": """+MainSettings.Bitcoin_MiningInfo + "\"" +"\n"+
            @"}";
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            MiningInfoQuery? miningInfo = JsonSerializer.Deserialize<MiningInfoQuery>(response.Content);
            if (miningInfo != null)
            {
                MiningInfo readminingdatafromdb = (from i in _db.MiningInfo select i).FirstOrDefault();
                if (readminingdatafromdb != null)
                {
                    readminingdatafromdb.blocks = miningInfo.result.blocks;
                    readminingdatafromdb.chain = miningInfo.result.chain;
                    readminingdatafromdb.pooledtx = miningInfo.result.pooledtx;
                    readminingdatafromdb.difficulty = miningInfo.result.difficulty;
                    readminingdatafromdb.networkhashps = miningInfo.result.networkhashps;
                    readminingdatafromdb.warnings = miningInfo.result.warnings;
                    readminingdatafromdb.Update = DateTime.UtcNow;
                    _db.MiningInfo.Update(readminingdatafromdb);
                }
                else
                {
                    MiningInfo create_new = new MiningInfo();
                    create_new._id = ObjectId.GenerateNewId();
                    create_new.blocks = miningInfo.result.blocks;
                    create_new.chain = miningInfo.result.chain;
                    create_new.pooledtx = miningInfo.result.pooledtx;
                    create_new.difficulty = miningInfo.result.difficulty;
                    create_new.networkhashps = miningInfo.result.networkhashps;
                    create_new.warnings = miningInfo.result.warnings;
                    create_new.Update = DateTime.UtcNow;
                    _db.MiningInfo.Add(create_new);
                }
            }
            Console.WriteLine($"{DateTime.UtcNow} Blockchain bitcoin mininginfo: Complete");
            _db.SaveChanges();
        }

    }
}
