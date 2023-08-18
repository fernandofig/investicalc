using FluentValidation;
using InvestmentCalculator.Domain.DataContracts;

namespace InvestmentCalculator.WebAPI.Validators;

public class RevenueRequestValidator : AbstractValidator<RevenueRequest>
{
	public RevenueRequestValidator()
	{
		RuleFor(r => r.Investment)
			.Must(v => v > 0)
			.WithMessage("Deve ser informado um valor positivo para o valor do Investimento");

		RuleFor(r => r.Months)
			.Must(m => m > 1)
			.WithMessage("O período da aplicação deve ser superior a 1 mês");
	}
}
