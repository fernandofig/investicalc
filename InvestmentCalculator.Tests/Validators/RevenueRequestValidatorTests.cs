using FluentValidation;
using FluentValidation.TestHelper;
using InvestmentCalculator.Domain.DataContracts;
using InvestmentCalculator.Tests.AutoFixture;
using InvestmentCalculator.WebAPI.Validators;

namespace InvestmentCalculator.Tests.Infrastructure;

public class RevenueRequestValidatorTests
{
	private readonly RevenueRequestValidator _sut;

	public RevenueRequestValidatorTests()
	{
		_sut = new RevenueRequestValidator();
	}

	[Fact]
	public void RevenueRequestValidator_ShouldBe_Derived_From_AbstractValidator()
	{
		Assert.IsAssignableFrom<AbstractValidator<RevenueRequest>>(_sut);
	}

	[Theory]
	[AutoInlineData(0.00001)]
	[AutoInlineData(1)]
	[AutoInlineData(1000)]
	[AutoInlineData(9999999999999)]
	public void RevenueRequestValidator_Should_Pass_If_Investment_Is_Greater_Than_Zero(
        decimal investmentValue,
		RevenueRequest request)
    {
		// Arrange
		request.Amount = investmentValue;
		request.Months = 18;

		// Act
		var result = _sut.TestValidate(request);

		// Assert
		result.ShouldNotHaveAnyValidationErrors();
    }

	[Theory]
	[AutoInlineData(2)]
	[AutoInlineData(12)]
	[AutoInlineData(180)]
	public void RevenueRequestValidator_Should_Pass_If_MonthsPeriod_Is_Greater_Than_One(
		uint months,
		RevenueRequest request)
	{
		// Arrange
		request.Amount = 20000;
		request.Months = months;

		// Act
		var result = _sut.TestValidate(request);

		// Assert
		result.ShouldNotHaveAnyValidationErrors();
	}

	[Theory]
	[AutoInlineData(0)]
	[AutoInlineData(-0.00001)]
	[AutoInlineData(-1000)]
	public void RevenueRequestValidator_Should_NotPass_If_Investment_Is_Zero_Or_Negative_Value(
		decimal investmentValue,
		RevenueRequest request)
	{
		// Arrange
		request.Amount = investmentValue;
		request.Months = 18;

		// Act
		var result = _sut.TestValidate(request);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Amount);
	}

	[Theory]
	[AutoInlineData(1)]
	[AutoInlineData(0)]
	public void RevenueRequestValidator_Should_NotPass_If_MonthsPeriod_Is_Less_Than_Two(
		uint months,
		RevenueRequest request)
	{
		// Arrange
		request.Amount = 20000;
		request.Months = months;

		// Act
		var result = _sut.TestValidate(request);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Months);
	}
}