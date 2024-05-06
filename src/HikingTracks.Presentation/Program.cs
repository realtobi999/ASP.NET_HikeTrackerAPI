using HikingTracks.Domain.Interfaces;
using HikingTracks.Presentation.Extensions;
using NLog;

namespace HikingTracks.Presentation
{
    public class Program
    {
        private static void Main(string[] args)
        {
            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            var builder = WebApplication.CreateBuilder(args);
            {
                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.ConfigureCors();
                builder.Services.ConfigureLoggerService();
                builder.Services.ConfigureRepositoryManager();
                builder.Services.ConfigureServiceManager();
                builder.Services.ConfigureDbContext();
                builder.Services.ConfigureJwtAuthentication(builder.Configuration);
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
                app.UseAuthorization();
                app.UseAccountMiddleware();

                if (app.Environment.IsDevelopment())
                    app.MapControllers().AllowAnonymous();
                else
                    app.MapControllers();

                app.Run();
            }
        }
    }
}