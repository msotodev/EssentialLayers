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

#### Release Notes
 - Now, the status "Created" is considered to be a successful response and "Unauthorized" to notify by message "Unauthorized" `14/04/2025`
 - Reverted dependency type from Singleton to Scoped to allow multiple instances of the same service `05-03-2025`
 - The HttpService and RequestService classes are now public `21-02-2025`
 - Implementation of IHttpClientFactory to better dependency manage of the "HttpClient" `18-02-2025`
 - It was solved the way of configure globally (ConfigureRequest) in the program file `23-01-2025`
 - Was solved the configuration issues to Http and Request services + RequestHelper + Logs implementation `13/12/2024`
 - It was added a new HttpHelper `12/12/2024`
 - It was removed the TRequest at the method GetAsync + Logs `06/12/2024`
 - Solved issue on serialize() & Added insensitiveMapping with default true `12/11/2024`
 - It was added a content type in a Request and changed the models location + Fixed reponse issue `05/11/2024`
 - It's added "CastResultAsResultHelper" parameter at HttpOption model in HttpService `29/10/2024`

Created by [Mario Soto Moreno](https://github.com/MatProgrammerSM)