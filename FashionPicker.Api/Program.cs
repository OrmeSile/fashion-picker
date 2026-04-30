using FashionPicker.Api.Endpoints;
using FashionPicker.Infrastructure;
using Microsoft.AspNetCore.HttpLogging;

var allowFrontendOrigin = "_allowFrontendOrigin";

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

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

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowFrontendOrigin, policy =>
    {
        policy.WithOrigins("https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services
    .AddOutfitContextPool(builder.Configuration)
    .AddInfraServices(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpLogging();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseCors(allowFrontendOrigin);

app.UseHttpsRedirection();

app.MapOutfitApiGroup();
app.MapClothingApiGroup();

app.Run();