using Microsoft.EntityFrameworkCore;

using OrderManagementWebAPI.DTOs;
using OrderManagementWebAPI.Repos.LabelsRepository;
using OrderManagementWebAPI.Repos.OrderLabelsRepository;
using OrderManagementWebAPI.Repos.OrdersRepository;
using OrderManagementWebAPI.Repos.OrderTracesRepository;
using OrderManagementWebAPI.Services.LabelsService;
using OrderManagementWebAPI.Services.OrderLabelsService;
using OrderManagementWebAPI.Services.OrdersService;
using OrderManagementWebAPI.Services.OrderTracesService;

namespace OrderManagementWebAPI
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

            builder.Services.AddDbContext<OrderManagementContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

            builder.Services.AddTransient<ILabelsRepo, LabelsRepo>();
            builder.Services.AddTransient<ILabelsService, LabelsService>();

            builder.Services.AddTransient<IOrdersRepo, OrdersRepo>();
            builder.Services.AddTransient<IOrdersService, OrdersService>();

            builder.Services.AddTransient<IOrderLabelsRepo, OrderLabelsRepo>();
            builder.Services.AddTransient<IOrderLabelsService, OrderLabelsService>();

            builder.Services.AddTransient<IOrderTracesRepo, OrderTracesRepo>();
            builder.Services.AddTransient<IOrderTracesService, OrderTracesService>();


            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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