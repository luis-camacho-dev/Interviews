
using Microsoft.EntityFrameworkCore;
using Data;
using ILogic;
using BusinessLogic;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions( x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ServiceRequestContext>(opt => opt.UseInMemoryDatabase("ServiceRequest"));

builder.Services.AddTransient<UnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IServiceRequestManager, ServiceRequestManager>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
