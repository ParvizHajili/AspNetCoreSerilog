using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Serilog.Events;
using System.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Log.Logger = new LoggerConfiguration().WriteTo.Seq(serverUrl: "http://localhost:5341", apiKey: "Ch4tni10V76ZqjhMgXVZ").CreateLogger();
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("AspNetCoreSerilog.txt", rollingInterval: RollingInterval.Hour).CreateLogger();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            Log.Error(error.Error.Message, "An unhandled exception has occurred while executing the request.");
        }
    });
});

app.Run();
