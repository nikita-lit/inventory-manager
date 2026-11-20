using IM.Data;
using Microsoft.EntityFrameworkCore;

namespace IM.Api;

public class Program
{
    public static void Main( string[] args )
    {
        var builder = WebApplication.CreateBuilder( args );

        var connectionString = builder.Configuration.GetConnectionString( "Inventory" )
            ?? throw new InvalidOperationException( "ConnectionStrings:Inventory is required." );
        builder.Services.AddDbContext<InventoryDbContext>( options => options.UseNpgsql( connectionString ) );

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        if ( builder.Configuration.GetValue<bool>( "Database:ApplyMigrations" ) )
        {
            using var scope = app.Services.CreateScope();
            scope.ServiceProvider.GetRequiredService<InventoryDbContext>().Database.Migrate();
        }

        // Configure the HTTP request pipeline.
        if ( app.Environment.IsDevelopment() )
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet( "/weatherforecast", ( HttpContext httpContext ) =>
            {
                var forecast = Enumerable.Range( 1, 5 ).Select( index =>
                        new WeatherForecast
                        {
                            Date = DateOnly.FromDateTime( DateTime.Now.AddDays( index ) ),
                            TemperatureC = Random.Shared.Next( -20, 55 ),
                            Summary = summaries[Random.Shared.Next( summaries.Length )]
                        } )
                    .ToArray();
                return forecast;
            } )
            .WithName( "GetWeatherForecast" );

        app.Run();
    }
}
