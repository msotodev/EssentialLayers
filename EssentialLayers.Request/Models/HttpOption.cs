using static EssentialLayers.Request.Helpers.Types.HttpTypes;

namespace EssentialLayers.Request.Models
{
	public class HttpOption
	{
		public string AppName { get; set; } = "AppName";

		public string AppVersion { get; set; } = "1.0";

		public string BaseUri { get; set; } = string.Empty;

		public ResultType ResultType { get; set; } = ResultType.Object;

		public bool InsensitiveMapping { get; set; } = true;

		public string BearerToken { get; set; } = string.Empty;

		/**/

		public HttpOption()
		{
			AppName = "AppName";
			AppVersion = "1.0";
			BaseUri = string.Empty;
			ResultType = ResultType.Object;
			InsensitiveMapping = true;
		}

		public HttpOption(string appName, string appVersion)
		{
			AppName = appName;
			AppVersion = appVersion;
		}

		public HttpOption(string baseUri)
		{
			BaseUri = baseUri;
		}

		public HttpOption(bool insensitiveMapping)
		{
			InsensitiveMapping = insensitiveMapping;
		}
	}
}