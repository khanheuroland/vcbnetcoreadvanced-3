using System.Configuration;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Json;
using Serilog.Settings.Configuration;

var builder = WebApplication.CreateBuilder(args);

//builder.Logging.AddLog4Net("log4net.config"); //Add Log4Net provider

var configuration = new LoggerConfiguration();
configuration.WriteTo.File(new JsonFormatter(), "D:\\Log\\jsonLogFile.json");
configuration.WriteTo.Seq("http://localhost:5341", Serilog.Events.LogEventLevel.Warning);
Log.Logger = configuration.CreateLogger();
builder.Services.AddLogging(logBuilder=>logBuilder.AddSerilog());


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
