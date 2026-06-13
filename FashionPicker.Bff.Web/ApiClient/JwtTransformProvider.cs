using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace FashionPicker.Bff.Web.ApiClient;

public class JwtTransformProvider(IHttpContextAccessor httpContextAccessor) : ITransformProvider
{

    public void ValidateRoute(TransformRouteValidationContext context)
    {
    }

    public void ValidateCluster(TransformClusterValidationContext context)
    {
    }

    public void Apply(TransformBuilderContext context)
    {
        if (context.Cluster?.ClusterId == "backend")
        {
            context.AddRequestTransform(async transformContext =>
            {
                if(httpContextAccessor.HttpContext is null)
                    throw new Exception("HttpContext not available");

                var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                transformContext.ProxyRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            });
        }
    }
}