using FashionPicker.Infra.DbContexts;
using FashionPicker.Infra.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionPicker.Infra;

public static class Extensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddOutfitContextPool(IConfiguration configuration)
        {
            return services.AddDbContextPool<OutfitDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("OutfitContext")));
        }

        public IServiceCollection AddInfraServices(IConfiguration configuration)
        {
            return services
                .AddScoped<OutfitProvider>();
        }
    }

}