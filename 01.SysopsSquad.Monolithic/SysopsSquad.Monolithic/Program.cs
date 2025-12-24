//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SysopsSquad.Monolithic.Components.Billing;
using SysopsSquad.Monolithic.Components.Customers;
using SysopsSquad.Monolithic.Components.Reporting;
using SysopsSquad.Monolithic.Components.Surveies.Notifies;
using SysopsSquad.Monolithic.Components.Tickets;
using SysopsSquad.Monolithic.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. Setup EF Core with In-Memory Database for the workshop
builder.Services.AddDbContext<SysopsSquadDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SysopsSquadCnn")));

// 2. Register all the monolithic component services
// In a monolith, all services are registered in one place.
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITicketAssignService, TicketAssignService>();
builder.Services.AddScoped<ICustomerProfileService, CustomerProfileService>();
builder.Services.AddScoped<IBillingService, BillingService>();
builder.Services.AddScoped<ISurveyNotifyService, SurveyNotifyService>();
builder.Services.AddScoped<IReportingService, ReportingService>();
// ... register other services here as you create them

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();


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