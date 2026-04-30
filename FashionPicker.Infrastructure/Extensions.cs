using FashionPicker.Core.Adapters;
using FashionPicker.Core.Repositories;
using FashionPicker.Infrastructure.Adapters.LocalCMS;
using FashionPicker.Infrastructure.DbContexts;
using FashionPicker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;

namespace FashionPicker.Infrastructure;

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
                .AddScoped<IOutfitRepository, OutfitRepository>()
                .AddScoped<IClothingRepository, ClothingRepository>()
                .AddScoped<ICmsAdapter, LocalCmsAdapter>();
        }
    }

    extension(IHeaderDictionary headers)
    {
        public IEnumerable<HttpHeader> Except(string headerKey)
        {
            return headers
                .Where(x => x.Key != headerKey)
                .Select(x => new HttpHeader(x.Key, x.Value));
        }
    }

    extension(HttpRequest httpRequest)
    {
        public StreamContent? StreamContent()
        {
            return httpRequest.ContentLength > 0 ? new StreamContent(httpRequest.Body) : null;
        }
    }

    extension(HttpResponse httpResponse)
    {
        public async Task CopyContentFrom(HttpContent content)
        {
            var stream = await content.ReadAsStreamAsync();
            await stream.CopyToAsync(httpResponse.Body);
        }
    }
}

public record HttpHeader(string Key, IEnumerable<string> Value)
{
    public static implicit operator HttpHeader(KeyValuePair<string, StringValues> header)
    {
        return new HttpHeader(header.Key, header.Value);
    }
}