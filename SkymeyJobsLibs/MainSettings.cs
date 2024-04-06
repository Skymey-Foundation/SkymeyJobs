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
    }

    public class BinanceAcualPrices : IBinanceAcualPrices
    {
        private IConfiguration? _configure;
        public static string URI { get; set; }
        public static string ActualPrices { get; set; }
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
            }
        }
    }

    public interface IBinanceAcualPrices
    {
        public static string? URI { get; set; }
        public static string? ActualPrices { get; set; }
        public void Init();
    }
}