using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;

const string authorizationPolicyName = "AllowIfAuthenticated";
const string corsPolicyName = "AllowCorsForClient";

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.MediaTypeOptions.AddText("multipart/form-data");
    logging.CombineLogs = true;
});
#endif

builder.Services
    .AddCors(options =>
    {
        options.AddPolicy(
            name: corsPolicyName,
            policy =>
            {
                policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
            }
            );
    })
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration.GetSection("IdentityProvider")["Authority"];
        options.MetadataAddress = builder.Configuration.GetSection("IdentityProvider")["MetadataAddress"] ?? null!;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey =  true,
            ValidateIssuer = false,
            ValidIssuer = builder.Configuration.GetSection("IdentityProvider")["Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration.GetSection("IdentityProvider")["Audience"],
        };
    });

builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy(authorizationPolicyName, policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });


builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ApiGateway"));

#if DEBUG
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 512;
    logging.ResponseBodyLogLimit = 512;
    logging.MediaTypeOptions.AddText("multipart/form-data");
    logging.CombineLogs = true;
});
#endif

var app = builder.Build();

app.UseCors(corsPolicyName);

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpLogging();
app.MapReverseProxy();
app.Run();