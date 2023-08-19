using InvestmentCalculator.Domain.DTOs;
using InvestmentCalculator.Domain.Enums;
using InvestmentCalculator.Infrastructure.Interfaces;
using InvestmentCalculator.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace InvestmentCalculator.Tests.Infrastructure;

public class RevenueCalculatorServiceTests
{
	private readonly IRateProvider _mockRateProvider;
	private readonly ILogger<RevenueCalculatorService> _mockLogger;
	private readonly RevenueCalculatorService _sut;

	public RevenueCalculatorServiceTests()
	{
		_mockRateProvider = Substitute.For<IRateProvider>();
		_mockLogger = Substitute.For<ILogger<RevenueCalculatorService>>();

		_sut = new RevenueCalculatorService(_mockRateProvider, _mockLogger);
	}

	[Fact]
	public void CalculateCDBRevenue_Should_Return_Populated_RevenueDTO_Object_If_No_Exceptions_Occur()
	{
		// Arrange
		var investmentDTO = new InvestmentDTO()
		{
			Investment = 20000,
			Months = 3
		};

		_mockRateProvider.GetRate(RateType.CDI, investmentDTO.Months).Returns(0);
		_mockRateProvider.GetRate(RateType.TB, investmentDTO.Months).Returns(0);
		_mockRateProvider.GetRate(RateType.VAT, investmentDTO.Months).Returns(0);
		
		// Act
		var result = _sut.CalculateCDBRevenue(investmentDTO);
		
		// Assert
		result.Should().BeOfType<RevenueDTO>();
		result.Should().NotBeNull();
		result.Gross.Should().Be(investmentDTO.Investment);
		result.Net.Should().Be(investmentDTO.Investment);
	}

	[Fact]
	public void CalculateCDBRevenue_Should_Return_Null_RevenueDTO_Object_If_Exceptions_Occur()
	{
		// Arrange
		var investmentDTO = new InvestmentDTO()
		{
			Investment = 20000,
			Months = 3
		};

		var ex = new InvalidOperationException("Service unavailable");

		_mockRateProvider.GetRate(RateType.CDI, investmentDTO.Months).Throws(ex);
		_mockRateProvider.GetRate(RateType.TB, investmentDTO.Months).Returns(0);
		_mockRateProvider.GetRate(RateType.VAT, investmentDTO.Months).Returns(0);

		// Act
		var result = _sut.CalculateCDBRevenue(investmentDTO);

		// Assert
		result.Should().BeNull();
		_mockLogger.ReceivedWithAnyArgs(1).LogError(ex, "Error");
	}
}