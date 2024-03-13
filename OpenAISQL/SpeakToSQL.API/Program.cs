
using Microsoft.Extensions.DependencyInjection;
using SpeakToSQL.API.Services;

namespace SpeakToSQL.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            builder.Services.AddSingleton<AOAIClient>(new AOAIClient(
                configuration["OpenAI:Key"]!,
                configuration["OpenAI:Endpoint"]!,
                configuration["OpenAI:Model"]!));

            builder.WebHost.UseUrls("http://localhost:5212", "https://localhost:7212");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
