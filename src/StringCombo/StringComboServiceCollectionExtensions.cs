using Microsoft.Extensions.DependencyInjection;
using StringCombo.File;

namespace StringCombo;

public static class StringComboServiceCollectionExtensions
{
    public static IServiceCollection AddStringComboServices(this IServiceCollection services)
    {
        services.AddTransient<IFileReader, FileReader>();
        
        return services;
    }
}