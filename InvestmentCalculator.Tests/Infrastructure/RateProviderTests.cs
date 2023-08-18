using InvestmentCalculator.Infrastructure.Providers;

using FluentAssertions;
using InvestmentCalculator.Tests.AutoFixture;
using InvestmentCalculator.Infrastructure.Interfaces;
using InvestmentCalculator.Domain.Enums;

namespace InvestmentCalculator.Tests.Infrastructure;

public class RateProviderTests
{
	[Theory, AutoNSubstituteData]
	public void RateProvider_Should_Implement_IRateProvider(
		RateProvider sut)
	{
		Assert.IsAssignableFrom<IRateProvider>(sut);
	}

	[Theory]
	[AutoInlineData(1, 0.225)]
	[AutoInlineData(5, 0.225)]
	[AutoInlineData(6, 0.225)]
	[AutoInlineData(7, 0.2)]
	[AutoInlineData(11, 0.2)]
	[AutoInlineData(12, 0.2)]
	[AutoInlineData(13, 0.175)]
	[AutoInlineData(11, 0.2)]
	[AutoInlineData(12, 0.2)]
	[AutoInlineData(13, 0.175)]
	[AutoInlineData(23, 0.175)]
	[AutoInlineData(24, 0.175)]
	[AutoInlineData(25, 0.15)]
	[AutoInlineData(25, 0.15)]
	[AutoInlineData(999, 0.15)]
	public void GetRate_ForVAT_Should_Return_Correct_Rate_According_To_Period(
        int months,
        decimal expectedRate,
	    RateProvider sut)
    {
		var rate = sut.GetRate(Domain.Enums.RateType.VAT, months);

		rate.Should().Be(expectedRate);
    }

	[Theory]
	[AutoInlineData(RateType.CDI, 1.08)]
	[AutoInlineData(RateType.TB, 0.009)]
	public void GetRate_ForOtherTypes_Should_Return_Fixed_Rate_According_To_Specification(
		RateType rateType,
		decimal expectedRate,
		RateProvider sut)
	{
		var rate = sut.GetRate(rateType, 2);

		rate.Should().Be(expectedRate);
	}

	[Fact]
	public void GetRate_WithUndefinedType_Should_ThrowException()
	{
		var sut = new RateProvider();

		Assert.Throws<KeyNotFoundException>(() => sut.GetRate(RateType.Undefined, 2));
	}
}