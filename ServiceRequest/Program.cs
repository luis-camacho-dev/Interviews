
using Microsoft.EntityFrameworkCore;
using Data;
using ILogic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ServiceRequestContext>(opt => opt.UseInMemoryDatabase("ServiceRequest"));
builder.Services.AddTransient<IServiceRequestManager, IServiceRequestManager>();
builder.Services.AddTransient<UnitOfWork, UnitOfWork>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
