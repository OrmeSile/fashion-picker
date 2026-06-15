using System.Security.Claims;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace Fashionpicker.ApiGateway.ApiAuth;

public class UserIdExtractorProvider: ITransformProvider
{
    private readonly ILogger<UserIdExtractorProvider> _logger;

    public UserIdExtractorProvider(ILogger<UserIdExtractorProvider> logger)
    {
        _logger = logger;
    }

    public void ValidateRoute(TransformRouteValidationContext context)
    {
    }

    public void ValidateCluster(TransformClusterValidationContext context)
    {
    }

    public void Apply(TransformBuilderContext context)
    {
        if (context.Route.RouteId is not ("api" or "file-repository"))
            return;
        context.AddRequestTransform(ctx =>
        {
            var id = ctx.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (id is null)
            {
                ctx.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return default;
            }
            ctx.Query.Collection["userId"] = id;
            return default;
        });
    }
}