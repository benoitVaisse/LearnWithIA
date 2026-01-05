using LearnWithIA.Infrastucture.Data;
using Microsoft.Extensions.DependencyInjection;

namespace LearnWithIA.Infrastucture;

public static class ConfigureQDRant
{
    public static async Task<IServiceCollection> AddQDRand(this IServiceCollection service, QDRandDatabase qDRanTContext)
    {
        service.AddScoped<QDRandDatabase, QDRandDatabase>();
        await qDRanTContext.EnsureCollectionExistsAsync();
        return service;
    }
}
