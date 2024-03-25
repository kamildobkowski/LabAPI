using System.Net;
using System.Net.Mail;
using LabAPI.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LabAPI.Infrastructure.Services.Email;

public sealed class EmailService(IConfiguration configuration) : IEmailService
{
	public async Task SendResultReadyEmail(string email, string name, string surname)
	{
		try
		{
			var body = $"""
			                    Witaj {name} {surname},
			                    Pojawiły się nowe wyniki związane z twoim kontem. Zaloguj się, aby je pobrać
			            """;

			await SendEmail("Wyniki gotowe", email, body, name, surname);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}
		
	}

	private async Task SendEmail(string subject, string email, string body, string name, string surname)
	{
		var fromMail = Environment.GetEnvironmentVariable("EMAIL_ADDRESS");
		var fromPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");

		if (fromMail is null || fromPassword is null)
			throw new Exception();

		var fromAddress = new MailAddress(fromMail, "LabAPI");
		var toAddress = new MailAddress(email, $"{name} {surname}");

		var smtp = new SmtpClient
		{
			Host = Environment.GetEnvironmentVariable("EMAIL_SMTP")!,
			Port = 587,
			EnableSsl = true,
			DeliveryMethod = SmtpDeliveryMethod.Network,
			UseDefaultCredentials = false,
			Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
		};
		using var message = new MailMessage(fromAddress, toAddress);
		message.Subject = subject;
		message.Body = body;
		await smtp.SendMailAsync(message);
	}
}