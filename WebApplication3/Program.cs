
using MessageBus;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Contracts;
using WebApplication3.Data;
using WebApplication3.Mapper;
using WebApplication3.Repostiory;

namespace WebApplication3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.\

            builder.Services.AddDbContext<MyDbContext>(options =>
            options.UseInMemoryDatabase(databaseName: "MyInMemoryDb"));

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IMessagingBus, MessagingBus>();

            builder.Services.AddAutoMapper(typeof(MappingConfig));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


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
