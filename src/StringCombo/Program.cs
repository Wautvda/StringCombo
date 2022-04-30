using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using StringCombo;
using StringCombo.Services;

var host =
    Host.CreateDefaultBuilder(args)
        .UseSerilog((_, configuration) =>
            {
                configuration
                    .Enrich.FromLogContext()
                    .WriteTo.File(
                        $"Logs/output.txt"
                        , restrictedToMinimumLevel: LogEventLevel.Information
                    )
                    .WriteTo.Console();
            }
        )
        .ConfigureServices(services =>
        {
            services
                .AddTransient<WordCombinationService>()
                .AddStringComboServices();
        })
        .Build();

var wordCombinationService = host.Services.GetRequiredService<WordCombinationService>();
await wordCombinationService.GetCombinationsAsync();

Console.WriteLine("press enter to exit");
Console.ReadLine();