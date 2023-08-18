using AutoFixture.Xunit2;

namespace InvestmentCalculator.Tests.AutoFixture;

public class AutoInlineDataAttribute : CompositeDataAttribute
{
	public AutoInlineDataAttribute(params object[] values)
		: base(new InlineDataAttribute(values), new AutoNSubstituteDataAttribute())
	{
	}
}
