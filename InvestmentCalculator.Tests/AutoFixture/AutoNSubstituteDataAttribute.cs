using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InvestmentCalculator.Tests.AutoFixture;

public class AutoNSubstituteDataAttribute : AutoDataAttribute
{
	public AutoNSubstituteDataAttribute()
		: base(FixtureFactory)
	{
	}

	public static IFixture FixtureFactory()
	{
		var fixture = new Fixture
		{
			RepeatCount = 1
		};

		fixture
			.Customize(new AutoNSubstituteCustomization { ConfigureMembers = true })
			.Customize<BindingInfo>(c => c.OmitAutoProperties());

		return fixture;
	}
}