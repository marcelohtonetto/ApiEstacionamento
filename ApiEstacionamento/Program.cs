
using ApiEstacionamento.Data;
using ApiEstacionamento.Repositorios;
using ApiEstacionamento.Repositorios.Interfaces;
using ApiEstacionamento.Services;
using ApiEstacionamento.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiEstacionamento
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


            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<EstacionamentoDBContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );


            builder.Services.AddScoped<IPrecoHoraServices, PrecoHoraServices>();
            builder.Services.AddScoped<IVeiculoServices, VeiculoServices>();
            builder.Services.AddScoped<IVeiculoRepositorio, VeiculoRepositorio>();
            builder.Services.AddScoped<IPrecoHoraRepositorio, PrecoHoraRepositorio>();

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
