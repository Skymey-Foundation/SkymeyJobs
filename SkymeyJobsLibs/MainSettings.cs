using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SkymeyJobsLibs
{
    public class MainSettingsFile
    {
        public string URI { get; set; }
        public string ActualPrices { get; set; }
        public string URI_Okex { get; set; }
        public string ActualPrices_Okex { get; set; }
        public string BinanceURIV3 { get; set; }
        public string BinanceTickerList { get; set; }
        public string OkexTickerListURI { get; set; }
        public string OkexTickerListSPOT { get; set; }
        public string OkexTickerListMARGIN { get; set; }
        public string OkexTickerListSWAP { get; set; }
        public string OkexTickerListFUTURES { get; set; }
        public string OkexTickerListOPTION { get; set; }
        public string CMC_URI { get; set; }
        public string CMC_MAP { get; set; }
        public string CMC_API { get; set; }
        public string Bitcoin_URI { get; set; }
        public string Bitcoin_MiningInfo { get; set; }
        public string BitcoinAuth { get; set; }
        public string Etherscan { get; set; }
        public string Moonscan { get; set; }
        public string EtherscanAPIKEY { get; set; }
        public string MoonscanAPIKEY { get; set; }
    }

    public class MainSettings : IMainSettings
    {
        private IConfiguration? _configure;
        public static string URI { get; set; }
        public static string ActualPrices { get; set; }
        public static string URI_Okex { get; set; }
        public static string ActualPrices_Okex { get; set; }
        public static string BinanceURIV3 { get; set; }
        public static string BinanceTickerList { get; set; }
        public static string OkexTickerListURI { get; set; }
        public static string OkexTickerListSPOT { get; set; }
        public static string OkexTickerListMARGIN { get; set; }
        public static string OkexTickerListSWAP { get; set; }
        public static string OkexTickerListFUTURES { get; set; }
        public static string OkexTickerListOPTION { get; set; }
        public static string CMC_URI { get; set; }
        public static string CMC_MAP { get; set; }
        public static string CMC_API { get; set; }
        public static string Bitcoin_URI { get; set; }
        public static string Bitcoin_MiningInfo { get; set; }
        public static string BitcoinAuth { get; set; }
        public static string Etherscan { get; set; }
        public static string Moonscan { get; set; }
        public static string EtherscanAPIKEY { get; set; }
        public static string MoonscanAPIKEY { get; set; }
        public MainSettings(IConfiguration? configure)
        {
            _configure = configure;
            Config.Path = _configure.GetSection("SettingsPath").Value;
            Config.MongoDbDatabase = _configure.GetSection("MongoDbDatabase").Value;
            Config.MongoClientConnection = _configure.GetSection("MongoClientConnection").Value;
        }
        public void Init()
        {
            var json = JsonSerializer.Deserialize<MainSettingsFile>(File.ReadAllText(Config.Path));
            if (json != null)
            {
                URI = json.URI;
                ActualPrices = json.ActualPrices;
                URI_Okex = json.URI_Okex;
                ActualPrices_Okex = json.ActualPrices_Okex;
                BinanceURIV3 = json.BinanceURIV3;
                BinanceTickerList = json.BinanceTickerList;
                OkexTickerListURI = json.OkexTickerListURI;
                OkexTickerListSPOT = json.OkexTickerListSPOT;
                OkexTickerListMARGIN = json.OkexTickerListMARGIN;
                OkexTickerListSWAP = json.OkexTickerListSWAP;
                OkexTickerListFUTURES = json.OkexTickerListFUTURES;
                OkexTickerListOPTION = json.OkexTickerListOPTION;
                CMC_URI = json.CMC_URI;
                CMC_MAP = json.CMC_MAP;
                CMC_API = json.CMC_API;
                Bitcoin_URI = json.Bitcoin_URI;
                Bitcoin_MiningInfo = json.Bitcoin_MiningInfo;
                BitcoinAuth = json.BitcoinAuth;
                Etherscan = json.Etherscan;
                Moonscan = json.Moonscan;
                EtherscanAPIKEY = json.EtherscanAPIKEY;
                MoonscanAPIKEY = json.MoonscanAPIKEY;
            }
        }
    }

    public interface IMainSettings
    {
        public static string? URI { get; set; }
        public static string? ActualPrices { get; set; }
        public static string? URI_Okex { get; set; }
        public static string? ActualPrices_Okex { get; set; }
        public static string? BinanceURIV3 { get; set; }
        public static string? BinanceTickerList { get; set; }
        public static string? OkexTickerListURI { get; set; }
        public static string? OkexTickerListSPOT { get; set; }
        public static string? OkexTickerListMARGIN { get; set; }
        public static string? OkexTickerListSWAP { get; set; }
        public static string? OkexTickerListFUTURES { get; set; }
        public static string? OkexTickerListOPTION { get; set; }
        public static string? CMC_URI { get; set; }
        public static string? CMC_MAP { get; set; }
        public static string? CMC_API { get; set; }
        public static string? Bitcoin_URI { get; set; }
        public static string? Bitcoin_MiningInfo { get; set; }
        public static string? BitcoinAuth { get; set; }
        public static string? Etherscan { get; set; }
        public static string? Moonscan { get; set; }
        public static string? EtherscanAPIKEY { get; set; }
        public static string? MoonscanAPIKEY { get; set; }
        public void Init();
    }
}