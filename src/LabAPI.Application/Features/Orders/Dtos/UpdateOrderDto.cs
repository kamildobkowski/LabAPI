using FluentValidation;
using LabAPI.Domain.Repositories;

namespace LabAPI.Application.Features.Orders.Dtos;

public sealed record UpdateOrderDto
{
	public string OrderNumber { get; init; } = null!;
	public List<string> Tests { get; init; } = [];
	public string? Pesel { get; init; }
	public DateOnly? DateOfBirth { get; init; }
	public string? Sex { get; init; }
	public string Name { get; init; } = null!;
	public string Surname { get; init; } = null!;
	public string AddressNumber { get; init; } = null!;
	public string AddressStreet { get; init; } = null!;
	public string AddressCity { get; init; } = null!;
	public string AddressPostalCode { get; init; } = null!;

}

public sealed class UpdateOrderDtoValidator : AbstractValidator<UpdateOrderDto>
{
	public UpdateOrderDtoValidator(IOrderRepository orderRepository, ITestRepository testRepository)
	{
		RuleFor(r => r.OrderNumber)
			.NotEmpty()
			.Length(10)
			.Custom((s, c) =>
			{
				var entity = orderRepository.GetAsync(r=>r.OrderNumber==s).Result;
				if(entity is null)
					c.AddFailure("Invalid order number");
			});
		RuleFor(r => r.Pesel)
			.Length(11)
			.Custom((r, s) =>
			{
				foreach (var i in r)
				{
					if (!char.IsDigit(i))
						s.AddFailure("Invalid PESEL");
				}
			});
		RuleFor(r => r.DateOfBirth)
			.LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));
		RuleFor(r => r.Sex)
			.Custom((s, c) =>
			{
				if (s is null) return;
				if (s.ToLower() != "m" && s.ToLower() != "f")
				{
					c.AddFailure("incorrect sex");
				}
			});
		RuleFor(r => r.Tests)
			.NotNull()
			.Custom((s, c) =>
			{
				foreach (var i in s)
				{
					var test = testRepository.GetAsync(r => r.Id == i).Result;
					if (test is null)
						c.AddFailure("Incorrect tests");
				}
			});
		RuleFor(r => r.AddressStreet)
			.NotEmpty();
		RuleFor(r => r.AddressCity)
			.NotEmpty();
		RuleFor(r => r.AddressNumber)
			.NotEmpty();
		RuleFor(r => r.AddressPostalCode)
			.Matches(@"^\d{2}-\d{3}$");
	}
}