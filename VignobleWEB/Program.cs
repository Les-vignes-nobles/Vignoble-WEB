using VignobleWEB.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
//Ajout du système de log
Log.Logger = new LoggerConfiguration()
    .CreateLogger();

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddConfiguration(builder.Configuration);
builder.Services.AddCustomServices();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

app.UseSession();

app.MapRazorPages();

app.Run();
