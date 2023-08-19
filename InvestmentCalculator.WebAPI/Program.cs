using FluentValidation;
using FluentValidation.AspNetCore;
using InvestmentCalculator.Infrastructure.Interfaces;
using InvestmentCalculator.Infrastructure.MappingProfiles;
using InvestmentCalculator.Infrastructure.Providers;
using InvestmentCalculator.Services;
using InvestmentCalculator.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IRevenueCalculatorService, RevenueCalculatorService>();
builder.Services.AddTransient<IRateProvider, RateProvider>();

builder.Services.AddControllers();

builder.Services
    .AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<Program>()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddAutoMapper(m => m.AddProfile<DataContractsMapping>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
