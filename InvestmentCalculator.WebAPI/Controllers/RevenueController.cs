using AutoMapper;
using InvestmentCalculator.Domain.DataContracts;
using InvestmentCalculator.Domain.DTOs;
using InvestmentCalculator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentCalculator.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RevenueController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRevenueCalculatorService _revenueCalculatorService;

	public RevenueController(IMapper mapper, IRevenueCalculatorService revenueCalculatorService)
    {
		_mapper = mapper;
        _revenueCalculatorService = revenueCalculatorService;
	}

    [HttpGet]
    public IActionResult GetCDBRevenue([FromQuery] RevenueRequest request)
    {
        var investmentDTO = _mapper.Map<InvestmentDTO>(request);

        var revenueDTO = _revenueCalculatorService.CalculateCDBRevenue(investmentDTO);

        if (revenueDTO == null)
            return StatusCode(StatusCodes.Status500InternalServerError);

        var revenueResp = _mapper.Map<RevenueResponse>(revenueDTO);

        return Ok(revenueResp);
    }
}
