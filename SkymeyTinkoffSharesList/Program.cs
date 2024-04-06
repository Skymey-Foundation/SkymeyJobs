using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SkymeyJobsLibs;
using SkymeyTinkoffSharesList.Actions.GetShares.Tinkoff;
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
                    services.AddSingleton<IMainSettingsStocks, MainSettingsStocks>();
                    services.AddSingleton<GetShares>();
                    services.AddSingleton<IHostedService, MySpecialService>();
                    using var serviceProvider = services.BuildServiceProvider();
                    var TinkoffService = serviceProvider.GetRequiredService<IMainSettingsStocks>();
                    TinkoffService.Init();
                });
await builder.RunConsoleAsync();
public class MySpecialService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await GetShares.GetSharesFromTinkoff();
                await Task.Delay(TimeSpan.FromHours(24));
            }
            catch (Exception ex)
            {
            }
        }
    }
}