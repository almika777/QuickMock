using Common.Options;
using Core.Extensions;
using Microsoft.OpenApi.Models;

namespace QuickMock
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "QuickMock API", Version = "v1" });
            });

            builder.Services.AddCore(builder.Configuration);
            builder.Services.Configure<AppOptions>(builder.Configuration.GetSection(nameof(AppOptions)));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.MapControllers();
            app.MapSwagger();
            app.UseSwaggerUI(x => { x.SwaggerEndpoint("v1/swagger.json", "QuickMock API V1"); });

            app.Run();
        }
    }
}