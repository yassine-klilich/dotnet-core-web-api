using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using PracticeWebAPI;
using PracticeWebAPI.DbContexts;
using PracticeWebAPI.Services;
using Serilog;

// Add Serilog logger configurations
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("log/pet-store.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

// Set Serilog logger service
builder.Host.UseSerilog();

// Add services to the container.

// Add basic controllers services to build an APIs
builder.Services.AddControllers(options =>
{ 
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()  // AddNewtonsoftJson function changes the default input/output JSON formatter.
.AddXmlDataContractSerializerFormatters();

// Register a custom service
#if DEBUG
builder.Services.AddTransient<IMailService, LocalMailService>();
#else
builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

builder.Services.AddSingleton<PetStoreDataStore>();

// Register PetStoreDbContext
builder.Services.AddDbContext<PetStoreDbContext>(
    options => options.UseSqlServer(
        builder.Configuration["ConnectionStrings:PetStoreDB"]
    )
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

//app.MapControllers();

app.Run();
