using Application.IRepository;
using Application.IService;
using Application.Service;
using Infrastructure.Helper.Redis;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

using Newtonsoft.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllersWithViews()
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
//builder.Services.AddControllers()
//        .AddNewtonsoftJson(options =>
//        {
//            options.SerializerSettings.ContractResolver = new DefaultContractResolver
//            {
//                NamingStrategy = new CamelCaseNamingStrategy()
//            };
//        });

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        var settings = options.SerializerSettings;
        settings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
        // dont mess with case of properties
        var resolver = options.SerializerSettings.ContractResolver as DefaultContractResolver;
        resolver.NamingStrategy = null;
    });

builder.Services.AddScoped<ICacheService, CacheService>();


//add service repositories
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();

builder.Services.AddScoped<ISetupService, SetupService>();
builder.Services.AddScoped<ISetupRepository, SetupRepository>();



builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("EnableCORS");

app.Run();
