﻿using AutoMapper;
using InvestmentCalculator.Domain.DataContracts;
using InvestmentCalculator.Domain.DTOs;

namespace InvestmentCalculator.Infrastructure.MappingProfiles;

public class DataContractsMapping : Profile
{
	public DataContractsMapping()
	{
		CreateMap<RevenueRequest, InvestmentDTO>();
		CreateMap<RevenueDTO, RevenueResponse>();
	}
}
