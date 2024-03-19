using FluentValidation;
using LabAPI.Application.Features.Orders.Repository;
using LabAPI.Application.Features.Tests.Repository;

namespace LabAPI.Application.Features.OrderResults.Dtos;

public sealed record CreateOrderResultDto
{
	public string OrderNumber { get; init; } = null!;
	public Dictionary<string, Dictionary<string, string>> Results { get; set; } = null!;
}

public sealed class CreateOrderResultDtoValidator : AbstractValidator<CreateOrderResultDto>
{
	public CreateOrderResultDtoValidator(IOrderRepository orderRepository, ITestRepository testRepository)
	{
		RuleFor(r => r.OrderNumber)
			.NotEmpty()
			.Length(10)
			.Custom((s, c) =>
			{
				var order = orderRepository.GetAsync(r => r.OrderNumber == s).Result;
				if (order is null)
					c.AddFailure("Invalid Order Number");
			});
		RuleFor(r => r.Results)
			.NotEmpty()
			.Custom((dic, c) =>
			{
				foreach (var i in dic)
				{
					var test = testRepository.GetAsync(r => r.ShortName == i.Key).Result;
					if (test is null)
					{
						c.AddFailure("Invalid Tests");
						return;
					}

					if (test.Markers.Count != i.Value.Count)
					{
						c.AddFailure("Invalid Tests");
						return;
					}

					foreach (var q in test.Markers)
					{
						if (i.Value.ContainsKey(q.ShortName)) continue;
						c.AddFailure("Invalid Tests");
						return;
					}
				}
			});
	}
}