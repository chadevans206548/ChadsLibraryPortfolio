using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ChadsLibraryPortfolio.Helpers;

public static class StartupExtensions
{
    public static void AddBasicServices(this IServiceCollection services)
    {

    }

    public static void AddAutoMappers(this IServiceCollection services)
    {
        var appMappingConfig = new MapperConfiguration(x =>
        {
            x.AddMaps("ChadsLibraryPortfolio.AutoMapper.Profiles");
        });

        var appMapper = appMappingConfig.CreateMapper();
        services.AddSingleton(appMapper);
    }
}
