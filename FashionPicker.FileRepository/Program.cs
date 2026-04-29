using FashionPicker.FileRepository.Interfaces;
using FileRepository;
using FileRepository.ConfigurationOptions;
using FileRepository.Services;
using FileRepository.Services.FileHandlers;
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
    .AddSingleton<StaticPathProvider>()
    .AddScoped<IFileStreamManager, FileStreamManager>()
    .AddScoped<MultipartFileService>()
    .AddScoped<ISimpleContentInspector, SimpleContentInspector>()
    .AddScoped<RepositoryFileInformationsProvider>()
    .AddScoped<IDataConsistencyChecker, DataConsistencyChecker>()
    .AddScoped<IImageOptimizer, NetVipsImageOptimizer>()
    .AddScoped<ImageHandler>()
    .AddFileRepositoryDbContext(builder.Configuration.GetConnectionString("FileRepositoryDbContext"));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseStaticFileRepositoryFiles();

app.MapControllers();
app.UseHttpsRedirection();

app.UseCors();

app.Run();
