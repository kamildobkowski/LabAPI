using Azure;
using Azure.Storage.Files.Shares;
using LabAPI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace LabAPI.Infrastructure.Repositories;

public sealed class PdfFileRepository(IConfiguration configuration) : IPdfFileRepository
{
	public async Task UploadFile(byte[] fileByteArray, string fileName)
	{
		var client =
			new ShareClient(configuration.GetConnectionString("AzureFileShare"), "orderpdfs");
		var directory = client.GetDirectoryClient("orderpdfs");
		
		directory.CreateIfNotExists();

		using var memoryStream = new MemoryStream(fileByteArray);
		if (await directory.ExistsAsync())
		{
			// Get a reference to a file and upload it
			ShareFileClient file = directory.GetFileClient(fileName);

			await file.CreateAsync(memoryStream.Length);
			await file.UploadAsync(memoryStream);
		}
	}
}