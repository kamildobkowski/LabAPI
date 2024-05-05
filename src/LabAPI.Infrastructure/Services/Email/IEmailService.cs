namespace LabAPI.Infrastructure.Services.Email;

public interface IEmailService
{
	Task SendResultReadyEmail(string email, string name, string surname);
}