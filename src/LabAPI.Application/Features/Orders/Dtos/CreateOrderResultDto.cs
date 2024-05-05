using FluentValidation;
using LabAPI.Domain.Repositories;

namespace LabAPI.Application.Features.Orders.Dtos;

public sealed record CreateOrderResultDto
{
	public string OrderNumber { get; init; } = null!;
	public Dictionary<string, Dictionary<string, string>?> Results { get; set; } = null!;
}

public sealed class CreateOrderResultDtoValidator : AbstractValidator<CreateOrderResultDto>
{
	public CreateOrderResultDtoValidator(IOrderRepository orderRepository, ITestRepository testRepository)
	{
		RuleFor(r => r.OrderNumber)
			.NotEmpty()
			.Length(10);
		RuleFor(r => r.Results)
			.NotNull()
			.Custom((dic, c) =>
			{
				foreach (var i in dic)
				{
					var test = testRepository.GetAsync(r => r.ShortName == i.Key).Result;
					if (test is null)
					{
						c.AddFailure("Invalid test name");
						return;
					}

					if (i.Value is null)
						return;
					
					if (test.Markers.Count != i.Value.Count)
					{
						c.AddFailure("Not enough/too many markers");
						return;
					}

					foreach (var q in test.Markers)
					{
						if (i.Value.ContainsKey(q.ShortName)) continue;
						c.AddFailure("Invalid marker name");
						return;
					}
				}
			});
		RuleFor(r => r)
			.Custom((val, c) =>
			{
				var order = orderRepository.GetAsync(r => r.OrderNumber == val.OrderNumber).Result;
				if (order is null){
					c.AddFailure("Order with given number does not exist");
					return;
				}

				if (order.Results.Count != val.Results.Count)
				{
					c.AddFailure("Not enough/too many results");
					return;
				}

				//change to not use linq
				foreach (var i in order.Results)
				{
					if (val.Results.ContainsKey(i.Key)) continue;
					c.AddFailure("Names of tests do not match");
					return;
				}
			});
	}
}