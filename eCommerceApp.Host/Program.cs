using eCommerceApp.Infrastructure.DependencieInjection;
using eCommerceApp.Application.DependencieInjection;
using Scalar.AspNetCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
Log.Logger.Information("Application is building......");
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(Options => 
    {
        Options.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

try
{
    var app = builder.Build();
    app.UseCors();
    app.UseSerilogRequestLogging();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
    }

    app.MapOpenApi();
    app.MapScalarApiReference();

    app.UseInfrastructureServices();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    Log.Logger.Information("Application is running.......");
    app.Run();
}
catch (Exception ex)
{
    Log.Logger.Fatal(ex, "Application failed to start.......");
}
finally
{
    Log.CloseAndFlush();
}
