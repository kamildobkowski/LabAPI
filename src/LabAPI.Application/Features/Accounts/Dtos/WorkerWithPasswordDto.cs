namespace LabAPI.Application.Features.Accounts.Dtos;

public sealed record WorkerWithPasswordDto(string Email, string Password, string Role, string Name, string Surname);