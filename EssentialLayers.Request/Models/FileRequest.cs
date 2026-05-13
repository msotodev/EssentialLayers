using System.IO;

namespace EssentialLayers.Request.Models
{
	public class FileRequest
	{
		public string Name { get; set; } = string.Empty;

		public string FileName { get; set; } = string.Empty;

		public Stream Stream { get; set; } = Stream.Null;
	}
}