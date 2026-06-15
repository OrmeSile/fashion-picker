using Duende.AccessTokenManagement.OpenIdConnect;
using Duende.IdentityModel.Client;
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
            context.AddRequestTransform(async transformContext =>
            {
                if (httpContextAccessor.HttpContext is null)
                    throw new Exception("HttpContext not available");

                var accessToken = await httpContextAccessor.HttpContext.GetUserAccessTokenAsync();
                if (!accessToken.Succeeded || accessToken.Token == null)
                {
                    transformContext.HttpContext.Response.StatusCode = 401;
                    return;
                }

                transformContext.ProxyRequest.SetBearerToken(accessToken.Token.AccessToken);
            });
    }
}