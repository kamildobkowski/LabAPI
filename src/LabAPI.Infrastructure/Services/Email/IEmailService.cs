namespace LabAPI.Application.Common.Interfaces;

public interface IEmailService
{
	Task SendResultReadyEmail(string email, string name, string surname);
}