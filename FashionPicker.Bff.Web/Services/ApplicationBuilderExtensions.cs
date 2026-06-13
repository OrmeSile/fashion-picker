using Microsoft.Net.Http.Headers;

namespace FashionPicker.Bff.Web.Services;

public static class ApplicationBuilderExtensions
{
    extension(IApplicationBuilder app)
    {
        public IApplicationBuilder UseNoUnauthorizedRedirect(params string[] segments)
        {
            app.Use(async (httpContext, func) =>
            {
                if (segments.Any(s => httpContext.Request.Path.StartsWithSegments(s)))
                {
                    httpContext.Request.Headers[HeaderNames.XRequestedWith] = "XMLHttpRequest";
                }

                await func();
            });

            return app;
        }
    }
}