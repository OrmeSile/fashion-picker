using FashionPicker.Core.Infra.Adapters.LocalCMS;
using FashionPicker.Core.Infra.DbContexts;
using FashionPicker.Core.Infra.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;

namespace FashionPicker.Core.Infra;

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
                .AddScoped<OutfitProvider>()
                .AddScoped<ClothingProvider>()
                .AddScoped<LocalCmsAdapter>();
        }
    }

    // https://medium.com/@vosarat1995/proxy-http-requests-in-net-9-like-a-pro-a238a7b261f7
    extension(HttpRequestMessage requestMessage)
    {
        public void AddHeaders(IEnumerable<HttpHeader> headers)
        {
            foreach (var header in headers)
            {
                requestMessage.AddHeader(header);
            }
        }

        public HttpHeader? AddHeader(HttpHeader header)
        {
            return requestMessage.AddRequestHeader(header) ?? requestMessage.AddContentHeader(header);
        }

        public HttpHeader? AddRequestHeader(HttpHeader candidate)
        {
            var success = requestMessage.Headers.TryAddWithoutValidation(candidate.Key, candidate.Value);
            return success ? candidate : null;
        }

        public HttpHeader? AddContentHeader(HttpHeader candidate)
        {
            var success = requestMessage.Content?.Headers.TryAddWithoutValidation(candidate.Key, candidate.Value) ?? false;
            return success ? new HttpHeader(candidate.Key, candidate.Value) : null;
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
    public static implicit operator HttpHeader(KeyValuePair<string, StringValues> header) => new(header.Key, header.Value);
}
