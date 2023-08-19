using InvestmentCalculator.Domain.DTOs;
using InvestmentCalculator.Domain.Enums;
using InvestmentCalculator.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace InvestmentCalculator.Services;

public class RevenueCalculatorService : Interfaces.IRevenueCalculatorService
{
	private readonly IRateProvider _rateProvider;
	private readonly ILogger<RevenueCalculatorService> _logger;

	public RevenueCalculatorService(IRateProvider rateProvider, ILogger<RevenueCalculatorService> logger)
	{
		_rateProvider = rateProvider;
		_logger = logger;
	}

	public RevenueDto CalculateCDBRevenue(InvestmentDto investmentDTO)
	{
		var revenue = investmentDTO.Amount;

		try
		{
			var cdiRate = _rateProvider.GetRate(RateType.CDI, investmentDTO.Months);
			var tbRate = _rateProvider.GetRate(RateType.TB, investmentDTO.Months);
			var taxRate = _rateProvider.GetRate(RateType.VAT, investmentDTO.Months);

			for (int m = 1; m <= investmentDTO.Months; m++)
			{
				revenue *= (1 + (cdiRate * tbRate));
			}

			return new RevenueDto()
			{
				Gross = revenue,
				Net = revenue - ((revenue - investmentDTO.Amount) * taxRate)
			};
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"[{nameof(RevenueCalculatorService)}.{nameof(CalculateCDBRevenue)}]");
			return null!;
		}
	}
}