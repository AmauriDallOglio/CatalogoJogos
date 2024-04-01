
using CatalogoJogos.Api.Controllers.V1;
using CatalogoJogos.Api.Middleware;
using CatalogoJogos.Nucleo.Interface;
using CatalogoJogos.Nucleo.Repositorio;
using CatalogoJogos.Nucleo.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CatalogoJogos.Api
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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CatalogoJogos", Version = "v1" });

                //propriedade da api / xml no debug de saida
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var fileName = typeof(Program).GetTypeInfo().Assembly.GetName().Name + ".xml";
                c.IncludeXmlComments(Path.Combine(basePath, fileName));
            });

            builder.Services.AddScoped<IJogoService, JogoService>();
            builder.Services.AddScoped<IJogoRepository, JogoSqlServerRepository>();


            //http://localhost:5024/api/v1/CicloDeVidaId
            builder.Services.AddSingleton<IExemploSingleton, ExemploCicloDeVida>();
            builder.Services.AddScoped<IExemploScoped, ExemploCicloDeVida>();
            builder.Services.AddTransient<IExemploTransient, ExemploCicloDeVida>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
