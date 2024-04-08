
using EmailAPI.Contracts;
using EmailAPI.Data;
using EmailAPI.Extension;
using EmailAPI.Messaging;
using EmailAPI.Repository;
using EmailAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmailAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("EmailConnectionString")));

            //var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionBuilder.UseNpgsql(builder.Configuration.GetConnectionString("EmailConnectionString"));
            //builder.Services.AddSingleton(new EmailService(optionBuilder.Options));

            builder.Services.AddSingleton<ApplicationDbContext>(provider =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("EmailConnectionString"));

                return new ApplicationDbContext(optionsBuilder.Options);
            });

            //builder.Services.AddSingleton<IEmailService>(provider =>
            //{
            //    var dbContext = provider.GetRequiredService<ApplicationDbContext>();
            //    return new EmailService(dbContext);
            //});

            builder.Services.AddSingleton<ILogEmailRepository>(provider =>
            {
                var dbContext = provider.GetRequiredService<ApplicationDbContext>();
                return new LogEmailRepository(dbContext);
            });

            builder.Services.AddSingleton<IEmailSender,  EmailSender>();
            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<IAzureConsumerBus, AzureConsumerBus>();
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
            app.UseAzureServiceBusConsumer();
            app.Run();
        }
    }
}
