using AutoMapper;
using InvestmentCalculator.Domain.DataContracts;
using InvestmentCalculator.Domain.DTOs;
using InvestmentCalculator.Services.Interfaces;
using InvestmentCalculator.Tests.AutoFixture;
using InvestmentCalculator.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace InvestmentCalculator.Tests.Infrastructure;

public class RevenueControllerTests
{
	private readonly IMapper _mockMapper;
	private readonly IRevenueCalculatorService _mockRevenueCalculatorService;
	private readonly RevenueController _sut;

	public RevenueControllerTests()
	{
		_mockMapper = Substitute.For<IMapper>();
		_mockRevenueCalculatorService = Substitute.For<IRevenueCalculatorService>();

		_sut = new RevenueController(_mockMapper, _mockRevenueCalculatorService);
	}

	[Theory, AutoNSubstituteData]
	public void GetCDBRevenue_Should_Return_200_When_Service_Method_Returns_NonNull_Object(
		RevenueRequest request,
		InvestmentDto investmentDTO,
		RevenueDto revenueDTO,
		RevenueResponse response
		)
	{
		// Arrange
		_mockMapper.Map<InvestmentDto>(request).Returns(investmentDTO);
		_mockRevenueCalculatorService.CalculateCDBRevenue(investmentDTO).ReturnsForAnyArgs(revenueDTO);
		_mockMapper.Map<RevenueResponse>(revenueDTO).Returns(response);

		// Act
		var result = _sut.GetCDBRevenue(request);
		
		// Assert
		result.Should().BeOfType<OkObjectResult>();
		result.As<OkObjectResult>().Value.Should().Be(response);
	}

	[Fact]
	public void GetCDBRevenue_Should_Return_500_When_Service_Method_Returns_Null_Object()
	{
		// Arrange
		_mockRevenueCalculatorService.CalculateCDBRevenue(new InvestmentDto()).ReturnsForAnyArgs((RevenueDto)null!);

		// Act
		var result = _sut.GetCDBRevenue(new RevenueRequest());

		// Assert
		result.Should().BeOfType<StatusCodeResult>();
		result.As<StatusCodeResult>().StatusCode.Should().Be(500);
	}
}