
using Microsoft.EntityFrameworkCore;
using PruebaTecnica_Cifrado_Homomorfico.Repository;
using PruebaTecnicaHomomorphicApis.DAL;

namespace PruebaTecnicaHomomorphicApis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var ConStr = builder.Configuration.GetConnectionString("ConStr");

            builder.Services.AddDbContext<Contexto>(options =>
            {
                options.UseSqlServer(ConStr);
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddScoped<ClientesRepository>();

            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(optcion =>
            {
                optcion.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                .AllowAnyMethod()
                );
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");


            app.MapControllers();

            app.Run();
        }
    }
}