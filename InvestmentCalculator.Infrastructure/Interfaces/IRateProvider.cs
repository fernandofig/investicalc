using InvestmentCalculator.Domain.Enums;

namespace InvestmentCalculator.Infrastructure.Interfaces;

public interface IRateProvider
{
	decimal GetRate(RateType type, uint monthsPeriod);
}
