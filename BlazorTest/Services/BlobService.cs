using EssentialLayers.AzureBlobs.Services.Blob;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;

namespace BlazorTest.Services
{
	public class BlobService (IAzureBlobService azureBlobService)
	{
		private readonly IAzureBlobService _azureBlobService = azureBlobService;

		public async Task<string?> UploadExampleAsync()
		{
			ResultHelper<string> result = await _azureBlobService.UploadFileAsync(
				"example.txt", "files", "HelloWorld".GetBytes()
			);

			if (result.Ok.False()) return null; // Message handling (result.Message)

			return result.Data; // Full url
		}

		public string? GetUrlFileExample()
		{
			ResultHelper<string> result = _azureBlobService.GetUrlFile(
				"example.txt", "files"
			);

			if (result.Ok.False()) return null; // Message handling (result.Message)

			return result.Data; // Full url
		}

		public async Task<string?> DownloadFileAsync()
		{
			ResultHelper<string> result = await _azureBlobService.DownloadFileAsync(
				"example.txt", "files"
			);

			if (result.Ok.False()) return null; // Message handling (result.Message)

			return result.Data; // Full url
		}

		public async Task<byte[]?> DownloadBytesAsync()
		{
			ResultHelper<byte[]> result = await _azureBlobService.DownloadBytesAsync(
				"example.txt", "files"
			);

			if (result.Ok.False()) return null; // Message handling (result.Message)

			return result.Data; // Full url
		}

		public async Task<bool> DeleteFileAsync()
		{
			ResultHelper<bool> result = await _azureBlobService.DeleteFileAsync(
				"example.txt", "files"
			);

			if (result.Ok.False()) return false; // Message handling (result.Message)

			return result.Data; // True or False if was deleted
		}
	}
}
