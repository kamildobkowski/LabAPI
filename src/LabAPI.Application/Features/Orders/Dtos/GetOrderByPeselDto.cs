using FluentValidation;
using LabAPI.Domain.Repositories;

namespace LabAPI.Application.Features.Orders.Dtos;

public sealed record GetOrderByPeselDto(string Pesel, string OrderNumber);

internal sealed class GetOrderByPeselDtoValidator : AbstractValidator<GetOrderByPeselDto>
{
	public GetOrderByPeselDtoValidator(IOrderRepository orderRepository)
	{
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
		RuleFor(r => r.OrderNumber)
			.NotEmpty()
			.Length(10);
		RuleFor(r => new { r.OrderNumber, r.Pesel })
			.Custom((s, context) =>
			{
				var order = orderRepository
					.GetAsync(r => r.OrderNumber == s.OrderNumber && r.PatientData.Pesel == s.Pesel).Result;
				if (order is null)
					context.AddFailure("Invalid Order Number or PESEL");
			});


	}
}