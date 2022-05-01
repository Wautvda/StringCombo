using Microsoft.Extensions.DependencyInjection;
using StringCombo.File;
using StringCombo.Models;
using StringCombo.Provider;
using StringCombo.Validators;

namespace StringCombo;

public static class StringComboServiceCollectionExtensions
{
    public static IServiceCollection AddStringComboServices(this IServiceCollection services)
    {
        services.AddTransient<IFileReader, FileReader>();
        services.AddTransient<IJoinableStringValidator, JoinableStringValidator>();
        services.AddTransient<ICombinableListProvider, CombinableListProvider>();
        services.AddTransient<IFileWriter, FileWriter>();
        
        return services;
    }
}