namespace LabAPI.Application.Common.Interfaces;

public interface IPdfFileRepository
{
	Task UploadFile(byte[] file, string fileName);
	Task<byte[]> GetFile(string fileName);
}