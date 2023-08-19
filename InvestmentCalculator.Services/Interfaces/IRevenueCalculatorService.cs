using InvestmentCalculator.Domain.DTOs;

namespace InvestmentCalculator.Services.Interfaces;

public interface IRevenueCalculatorService
{
	RevenueDto CalculateCDBRevenue(InvestmentDto investmentDTO);
}