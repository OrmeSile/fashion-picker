using FashionPicker.FileRepository;
using FashionPicker.FileRepository.Configuration;
using FashionPicker.FileRepository.Endpoints;
using FashionPicker.FileRepository.Interfaces;
using FashionPicker.FileRepository.Providers;
using FashionPicker.FileRepository.Services;
using FashionPicker.FileRepository.Services.FileHandlers;
using Microsoft.AspNetCore.HttpLogging;

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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("*")
            .AllowAnyHeader();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.CreateStaticFileFolderIfNotExists();
builder.Services
    .Configure<FileRepositoryOptions>(builder.Configuration.GetSection(FileRepositoryOptions.Section))
    .AddFileRepositoryDbContext(builder.Configuration.GetConnectionString("FileRepositoryDbContext"))
    .AddSingleton<StaticPathProvider>()
    .AddHostedService<DataConsistencyCheckerBackgroundServiceConsumer>()
    .AddScoped<IDataConsistencyChecker, DataConsistencyChecker>()
    .AddScoped<IFileStreamManager, FileStreamManager>()
    .AddScoped<MultipartFileService>()
    .AddScoped<ISimpleContentInspector, SimpleContentInspector>()
    .AddScoped<RepositoryFileInformationsProvider>()
    .AddScoped<IImageOptimizer, NetVipsImageOptimizer>()
    .AddScoped<ImageHandler>();


builder.Services.AddOpenApi();

var app = builder.Build();

#if DEBUG
app.UseHttpLogging();
#endif

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseStaticFileRepositoryFiles();

app.MapFileUploadGroup();
app.UseHttpsRedirection();

app.UseCors();

app.Run();