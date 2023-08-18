using InvestmentCalculator.Domain.Enums;
using InvestmentCalculator.Infrastructure.Interfaces;

namespace InvestmentCalculator.Infrastructure.Providers;

public class RateProvider : IRateProvider
{
	public decimal GetRate(RateType type, int monthsPeriod)
	{
		// Aqui poderia ser substituído por alguma lógica para obter a informação através de algum outro serviço ou store
		return type switch
		{
			RateType.CDI                            => 0.009m,
			RateType.TB                             => 1.08m,

			RateType.VAT when monthsPeriod <= 6  => 0.225m,
			RateType.VAT when monthsPeriod <= 12 => 0.2m,
			RateType.VAT when monthsPeriod <= 24 => 0.175m,
			RateType.VAT                            => 0.15m,

			_                                       => throw new KeyNotFoundException("Tipo de alíquota inválida")
		};
	}
}
