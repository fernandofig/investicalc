namespace InvestmentCalculator.Domain.DataContracts;

public class RevenueRequest
{
	public decimal Amount { get; set; }

	public uint Months { get; set; }
}
