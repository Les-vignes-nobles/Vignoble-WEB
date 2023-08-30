using VignobleWEB.Core.Application.Tools;
using VignobleWEB.Core.Application.Repositories;
using VignobleWEB.Core.Infrastructure.Tools;
using VignobleWEB.Core.Infrastructure.DataLayers;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VignobleWEB.Core.Interfaces.Infrastructure.Token;
using VignobleWEB.Core.Infrastructure.Token;

var builder = WebApplication.CreateBuilder(args);

//Ajout du système de log
Log.Logger = new LoggerConfiguration()
    .CreateLogger();

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

// Add services to the container.
builder.Services.AddRazorPages();

//Permet lors de l'utilisation d'une interface de table en base de données de le lier au DataLayer associé
builder.Services.AddScoped<IProductDataLayer, APIProductDataLayer>();
builder.Services.AddScoped<ITokenAPI, TokenAPI>();

//Permet lors de l'utilisation d'une interface de repository de le lier à son repository associé
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//Ajout du scope sur les Tools Infrastructure
builder.Services.AddScoped<ILogInfrastructure, LogInfrastructure>();

//Ajout du scope sur les Tools Repository
builder.Services.AddScoped<ILogRepository, LogRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    Log.Information("Environnement de PROD détecté");
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    Log.Information("Environnement de dev détecté");
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
