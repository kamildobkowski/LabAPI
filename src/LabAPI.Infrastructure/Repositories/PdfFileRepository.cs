using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using LabAPI.Application.Common.Interfaces;
using LabAPI.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace LabAPI.Infrastructure.Repositories;

public sealed class PdfFileRepository(ILogger<PdfFileRepository> logger) : IPdfFileRepository
{
	public async Task UploadFile(byte[] fileByteArray, string fileName)
	{
		try
		{
			var client =
				new ShareClient(Environment.GetEnvironmentVariable("AZUREFILESHARE_CONNECTIONSTRING"), "orderpdfs");
			var directory = client.GetDirectoryClient("orderpdfs");

			await directory.CreateIfNotExistsAsync();

			using var memoryStream = new MemoryStream(fileByteArray);
			if (await directory.ExistsAsync())
			{
				// Get a reference to a file and upload it
				ShareFileClient file = directory.GetFileClient(fileName);

				await file.CreateAsync(memoryStream.Length);
				await file.UploadAsync(memoryStream);
			}
			else throw new ApplicationException("Invalid pdf azure file storage");
			logger.LogInformation("Uploaded result pdf file to azure file storage");
		}
		catch (Exception e)
		{
			logger.LogInformation("Error while uploading pdf file to azure file storage");
			throw;
		}
		
	}

	public async Task<byte[]> GetFile(string fileName)
	{
		try
		{
			var client = new ShareClient(Environment.GetEnvironmentVariable("AZUREFILESHARE_CONNECTIONSTRING"),
				"orderpdfs");
			var directory = client.GetDirectoryClient("orderpdfs");

			ShareFileClient file = directory.GetFileClient(fileName);
			Response<ShareFileDownloadInfo> downloadResponse = await file.DownloadAsync();

			//check if file exists
			if (downloadResponse.Value.Content == null)
			{
				throw new ApplicationException("File not found in azure file storage");
			}

			//read data from file
			using var memoryStream = new MemoryStream();
			await downloadResponse.Value.Content.CopyToAsync(memoryStream);

			return memoryStream.ToArray();
		}
		catch (ApplicationException e)
		{
			logger.LogError(e.Message);
			throw new NotFoundException(e.Message);
		}
		catch (Exception e)
		{
			logger.LogError("Error while downloading pdf file from azure file storage" + e.Message);
			throw;
		}
		
	}
}