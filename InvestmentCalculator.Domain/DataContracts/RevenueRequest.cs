namespace InvestmentCalculator.Domain.DataContracts;

public class RevenueRequest
{
	public decimal Investment { get; set; }

	public uint Months { get; set; }
}
