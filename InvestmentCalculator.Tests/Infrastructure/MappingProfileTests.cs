using AutoMapper;
using InvestmentCalculator.Infrastructure.MappingProfiles;

namespace InvestmentCalculator.Tests.Infrastructure;

public class MappingProfileTests
{
	[Fact]
	public void Assert_DataContractsMapping_Profile_Configuration_IsValid()
	{
		var cfg = new MapperConfiguration(cfg => cfg.AddProfile<DataContractsMapping>());

		cfg.AssertConfigurationIsValid();
	}
}