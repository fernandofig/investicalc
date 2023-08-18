﻿using InvestmentCalculator.Domain.DTOs;
using InvestmentCalculator.Domain.Enums;
using InvestmentCalculator.Infrastructure.Interfaces;

namespace InvestmentCalculator.Services;

public class RevenueCalculatorService : Interfaces.IRevenueCalculatorService
{
	private readonly IRateProvider _rateProvider;

	public RevenueCalculatorService(IRateProvider rateProvider)
	{
		_rateProvider = rateProvider;
	}

	public RevenueDTO CalculateCDBRevenue(InvestmentDTO investmentDTO)
	{
		var revenue = investmentDTO.Investment;

		var cdiRate = _rateProvider.GetRate(RateType.CDI, investmentDTO.Months);
		var tbRate = _rateProvider.GetRate(RateType.TB, investmentDTO.Months);
		var taxRate = _rateProvider.GetRate(RateType.VAT, investmentDTO.Months);

		for (int m = 1; m <= investmentDTO.Months; m++)
		{
			revenue *= (1 + (cdiRate * tbRate));
		}

		return new RevenueDTO()
		{
			Gross = revenue,
			Net = revenue - (revenue * taxRate)
		};
	}
}