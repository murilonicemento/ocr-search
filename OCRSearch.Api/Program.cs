using OCRSearch.Application.Interfaces;
using OCRSearch.Application.Services;
using OCRSearch.Infrastructure.Configurations;
using OCRSearch.Infrastructure.Providers;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<ElasticSearchConfiguration>(builder.Configuration.GetSection("ElasticSearchConfiguration"));

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ISearchProvider, ElasticSearchProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();