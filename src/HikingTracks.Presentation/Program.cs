using System.Text.Json.Serialization;
using HikingTracks.Domain.Interfaces;
using HikingTracks.Presentation.Extensions;
using NLog;

namespace HikingTracks.Presentation;

public class Program
{
    private static void Main(string[] args)
    {
        LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

        var builder = WebApplication.CreateBuilder(args);
        {
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.ConfigureSwaggerGen();

            builder.Services.ConfigureCors();

            builder.Services.ConfigureLoggerService();
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureDbContext();

            builder.Services.ConfigureServiceManager();

            builder.Services.AddJWTAuthentication(builder.Configuration);
            builder.Services.AddAuthorization();
        }

        var app = builder.Build();
        {
            var logger = app.Services.GetRequiredService<ILoggerManager>();
            app.ConfigureExceptionHandler(logger);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseAccountAuthentication();
            app.UseHikeAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}