using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SkymeyBlockchainBitcoin.Actions.GetMiningInfo;
using SkymeyJobsLibs;


namespace SkymeyBlockchainBitcoin
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json");
                config.AddEnvironmentVariables();

                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.AddSingleton<Config>();
                    services.AddSingleton<IMainSettings, MainSettings>();
                    services.AddSingleton<GetMiningInfo>();
                    services.AddSingleton<IHostedService, MySpecialService>();
                    using var serviceProvider = services.BuildServiceProvider();
                    var BinanceService = serviceProvider.GetRequiredService<IMainSettings>();
                    BinanceService.Init();
                });
            await builder.RunConsoleAsync();
        }
    }
    public class MySpecialService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await GetMiningInfo.GetMiningDetails();
                    await Task.Delay(TimeSpan.FromHours(1));
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
