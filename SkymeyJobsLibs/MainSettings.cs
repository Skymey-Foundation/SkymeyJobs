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
        public void Init();
    }
}