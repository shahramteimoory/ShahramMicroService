using Catalog.Application.Interface;
using Catalog.Application.Services.Products;
using Catalog.Persistance;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var databaseSettings =builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();

// ثبت تنظیمات به عنوان یک سرویس
builder.Services.AddSingleton(databaseSettings);

// ثبت سایر سرویس‌ها
builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();

builder.Services.AddScoped<IProductManagmentService, ProductManagmentService>();
// Add services to the container.

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

app.UseAuthorization();

app.MapControllers();

app.Run();
