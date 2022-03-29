
using Microsoft.EntityFrameworkCore;
using Data;
using ILogic;
using BusinessLogic;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions( x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddDbContext<ServiceRequestContext>(opt => opt.UseInMemoryDatabase("ServiceRequest"));

builder.Services.AddTransient<UnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IServiceRequestManager, ServiceRequestManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
