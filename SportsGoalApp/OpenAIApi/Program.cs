
using ChatGPT.Net;
using Microsoft.Extensions.Options;
using OpenAIApi.Options;
using OpenAIApi.Wrappers;

namespace OpenAIApi
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
            builder.Services.AddHealthChecks();

            builder.Services.Configure<ChatGptSettings>(builder.Configuration.GetSection("ChatGpt"));
            builder.Services.AddSingleton<ChatGpt>(provider => {
                var settings = provider.GetRequiredService<IOptions<ChatGptSettings>>().Value;
                return new ChatGpt(settings.ApiKey);
                });
            builder.Services.AddScoped<IChatGpt, ChatGptWrapper>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapHealthChecks("/health");
            
            app.MapControllers();

            app.Run();
        }
    }
}
