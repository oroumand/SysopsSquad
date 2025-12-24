using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SysopsSquad.Monolithic.Components.Billing;
using SysopsSquad.Monolithic.Components.Customers;
using SysopsSquad.Monolithic.Components.Reporting;
using SysopsSquad.Monolithic.Components.SupportContracts;
using SysopsSquad.Monolithic.Components.Surveies.Notifies;
using SysopsSquad.Monolithic.Components.Tickets;
using SysopsSquad.Monolithic.Data;
using SysopsSquad.Monolithic.Infrastructure;
using SysopsSquad.Monolithic.Infrastructure.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. Setup EF Core with SQL Database for the workshop
builder.Services.AddDbContext<SysopsSquadDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SysopsSquadCnn")));



builder.Services.AddDbContext<ReportingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReportingCnn")));

// 2. Register all the monolithic component services
// In a monolith, all services are registered in one place.

// Register the Event Bus as a Singleton
builder.Services.AddSingleton<IEventBus, InMemoryEventBus>();
builder.Services.AddScoped<ReportingEventHandlers>(); // ثبت Handler
builder.Services.AddScoped<CustomerEventsHandler>(); 

builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITicketAssignService, TicketAssignService>();
builder.Services.AddScoped<ICustomerProfileService, CustomerProfileService>();
builder.Services.AddScoped<IBillingService, BillingService>();
builder.Services.AddScoped<ISurveyNotifyService, SurveyNotifyService>();
builder.Services.AddScoped<IReportingService, ReportingService>();
builder.Services.AddScoped<ISupportContractService, SupportContractService>();
// ... register other services here as you create them

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();


// *** SUBSCRIBE EVENT HANDLERS ***
// This is the "wiring" part.
using (var scope = app.Services.CreateScope())
{
    var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
    var handlers = scope.ServiceProvider.GetRequiredService<ReportingEventHandlers>();
    var customerhandlers = scope.ServiceProvider.GetRequiredService<CustomerEventsHandler>();

    eventBus.Subscribe<TicketCreatedEvent>(e => handlers.Handle(e));
    eventBus.Subscribe<CustomerRegisteredEvent>(e => handlers.Handle(e));
    eventBus.Subscribe<CustomerRegisteredEvent>(e => customerhandlers.Handle(e));
}

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