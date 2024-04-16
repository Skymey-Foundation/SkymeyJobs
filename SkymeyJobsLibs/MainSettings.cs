using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SkymeyJobsLibs
{
    public class Binance
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
    }

    public class BinanceAcualPrices : IBinanceAcualPrices
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
        public BinanceAcualPrices(IConfiguration? configure)
        {
            _configure = configure;
            Config.Path = _configure.GetSection("SettingsPath").Value;
            Config.MongoDbDatabase = _configure.GetSection("MongoDbDatabase").Value;
            Config.MongoClientConnection = _configure.GetSection("MongoClientConnection").Value;
        }
        public void Init()
        {
            var json = JsonSerializer.Deserialize<Binance>(File.ReadAllText(Config.Path));
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
            }
        }
    }

    public interface IBinanceAcualPrices
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
        public void Init();
    }
}