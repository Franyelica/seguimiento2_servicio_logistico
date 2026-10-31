using System.Text.Json.Serialization;
using SGCAE.Api.Middleware;
using SGCAE.Application;
using SGCAE.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    // Los enums se leen y se muestran como texto: "Entry", "Authorized"...
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();   // primero, para capturar errores de todo lo demás

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();