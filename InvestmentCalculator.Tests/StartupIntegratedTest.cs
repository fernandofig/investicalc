using InvestmentCalculator.Domain.DataContracts;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace InvestmentCalculator.Tests.Infrastructure;

public class StartupIntegratedTest
{
	[Fact]
	public async Task Assert_That_Application_Starts_And_Requests_Run()
	{
		var ex = await Record.ExceptionAsync(async () =>
		{
			var app = new WebApplicationFactory<Program>();
			var client = app.CreateClient();
			await client.GetFromJsonAsync<RevenueResponse>("/api/Revenue?investment=20000&months=3");
		});

		ex.Should().BeNull();
	}
}