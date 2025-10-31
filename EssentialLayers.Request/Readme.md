# Essential Layers
### EssentialLayers.Request

Is a complement to the package [EssentialLayers](/EssentialLayers/Readme.md) to provide an extra layer for using http requests in an easy way.

### Configure

Add the dependencies in your **Program.cs** file

```
builder.Services.UseRequest();
```

And then set the options (Optional)

```
app.Services.ConfigureRequest(
	new HttpOption
	{
		BaseUri = "https/api.dev",
		AppName = "MyApi",
		AppVersion = "v1",
		InsensitiveMapping = true
	}
);
```

If you want manage multiple instances of the same service, you can use the following code:

1. Inject the IHttpService or IRequestService
2. Set configs on runtime

```
yourService.SetOptions(
	new HttpOption
	{
		BaseUri = YOUR_BASE_URI,
		Token = YOUR_TOKEN
	}
);
```

## 🚀 New Feature: IHttpFactory

IHttpFactory is the new provider to send request toward multiple apis.

To start to use, add the next section in your  **appsettings.json**

```
"HttpClients": {
	"AuthApi": {
		"BaseUrl": "https://localhost:5000/api/",
		"UserAgent": "FirstApiClient/1.0", (Optional) => Default 'MyApp/1.0'
		"ContentType": "application/json" (Optional) => Default 'application/json'
	}
	"SecondApiClient": {
		"BaseUrl": "https://localhost:5001/api/",
		"UserAgent": "SecondApiClient/1.0", (Optional) => Default 'MyApp/1.0'
		"ContentType": "application/json" (Optional) => Default 'application/json'
	}
}
```

In your **Program.cs** file

```
builder.Services.AddHttpClients(builder.Configuration);
builder.Services.ConfigureFactory();
```

OR

In your **Program.cs** file

```
services.AddTransient<AuthHeaderHandler>();

services.AddHttpClient(
	clientName, (client) =>
	{
		client.BaseAddress = new Uri("https://localhost:5000/api/");
		client.DefaultRequestHeaders.UserAgent.ParseAdd("AuthApi/1.0");
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
	}
).AddHttpMessageHandler<AuthHeaderHandler>();

services.AddHttpClient(
	clientName, (client) =>
	{
		client.BaseAddress = new Uri("https://localhost:5001/api/");
		client.DefaultRequestHeaders.UserAgent.ParseAdd("SecondApiClient/1.0");
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
	}
).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.ConfigureFactory();
```

And in your specific **service.cs** file

```
public class AuthService (IHttpFactory httpFactory)
	{
		private const string CLIENT_NAME = "AuthApi";

		public Task<HttpResponse<LoginResponseDto>> LoginAsync(string userName, string password)
		{
			return httpFactory.PostAsync<LoginResponseDto, LoginRequestDto>(
				CLIENT_NAME, "User/Login", new LoginRequestDto(userName, password)
			);
		}
	}

	public record LoginRequestDto(string UserName, string Password);

	public record LoginResponseDto(UserDto? User, string Token);

	public record UserDto(string Id, string Email, string Name, string PhoneNumber);
```

#### Release Notes
 - Removed FactoryTokenProvider as a singleton `31-10-2025`
 - Implemented the IHttpFactory as a stateless, passing as parameter the clientName `24-10-2025`
 - The response from all methods has changed from ResultHelper<T> to HttpResponse<T> to keep compatibility `23-10-2025`
 - Now instead of clienName the key will be taking as a clientName `22-10-2025`
 - Read multiple http clients from the appsettings.json `22-10-2025`
 - "HttpFactoryOptions" configuration binded correctly `22-10-2025`
 - It was solved "Unable to resolve service for type 'EssentialLayers.Request.Services.Factory.IFactoryTokenProvider" issue `21-10-2025`
 - New provider based in IHttpClientFactory implementation to send requests `21-10-2025`
 - Updating of dependencies to the last version `14-10-2025`
 - In the model HttpOption, must be add an extra property to send the headers since ConfigureRequest `09/10/2025`
 - Updating of nuget packages EssentialLayers and Microsoft.Extensions.Http `27/08/2025`
 - Remove repeated headers `17-07-2025`
 - Now all methods of RequestService & RequestHelper allows nullable results; It was fixed issue in uribase `21-05-2025`
 - New Delete method in HttpService only with url `16-04-2025`
 - Reverted dependency type from Scoped to Singleton; Because always allows the overriding of base uri `14-04-2025`
 - Now, the status "Created" is considered to be a successful response and "Unauthorized" to notify by message "Unauthorized" `14/04/2025`
 - Reverted dependency type from Singleton to Scoped to allow multiple instances of the same service `05-03-2025`
 - The HttpService and RequestService classes are now public `21-02-2025`
 - Implementation of IHttpClientFactory to better dependency manage of the "HttpClient" `18-02-2025`
 - It was solved the way of configure globally (ConfigureRequest) in the program file `23-01-2025`
 - Was solved the configuration issues to Http and Request services + RequestHelper + Logs implementation `13/12/2024`
 - It was added a new HttpHelper `12/12/2024`
 - It was removed the TRequest at the method GetAsync + Logs `06/12/2024`
 - Solved issue on serialize() & Added insensitiveMapping with default true `12/11/2024`
 - It was added a content type in a Request and changed the models location + Fixed response issue `05/11/2024`
 - It's added "CastResultAsResultHelper" parameter at HttpOption model in HttpService `29/10/2024`

Created by [Mario Soto Moreno](https://github.com/MatProgrammerSM)