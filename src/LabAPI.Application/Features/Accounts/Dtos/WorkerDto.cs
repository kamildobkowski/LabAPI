namespace LabAPI.Application.Features.Accounts.Dtos;

public sealed record WorkerDto
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
}