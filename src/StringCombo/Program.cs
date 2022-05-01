using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using StringCombo;
using StringCombo.Models;
using StringCombo.Services;

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
    cts.Cancel();
    e.Cancel = true;
};
var host =
    Host.CreateDefaultBuilder(args)
        .UseSerilog((_, configuration) =>
            {
                configuration
                    .MinimumLevel.Debug()
                    .Enrich.FromLogContext()
                    .WriteTo.Console(LogEventLevel.Debug);
            }
        )
        .ConfigureServices(services =>
        {
            Parser.Default
                .ParseArguments<CommandOptions>(args)
                .WithParsed(options =>
                {
                    services.AddSingleton<IOptions<CommandOptions>>(_ => Options.Create(options));
                });
            services
                .AddTransient<WordCombinationService>()
                .AddStringComboServices();
        })
        .Build();

var wordCombinationService = host.Services.GetRequiredService<WordCombinationService>();
await Parser.Default
    .ParseArguments<CommandOptions>(args)
    .MapResult(
        async _ => await wordCombinationService.GetCombinationsAsync(cts.Token)
        ,_ => Task.FromResult(-1));

Console.WriteLine("press enter to exit");
Console.ReadLine();