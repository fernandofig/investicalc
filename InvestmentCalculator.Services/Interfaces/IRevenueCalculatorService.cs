using InvestmentCalculator.Domain.DTOs;

namespace InvestmentCalculator.Services.Interfaces;

public interface IRevenueCalculatorService
{
	RevenueDTO CalculateCDBRevenue(InvestmentDTO investmentDTO);
}