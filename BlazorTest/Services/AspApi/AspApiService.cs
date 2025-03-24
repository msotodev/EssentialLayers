namespace BlazorTest.Services.AspApi
{
	public class AspApiService (IConfiguration configuration) : IAspApiService
	{
		public string BaseUri => configuration.GetSection("Apis").GetValue<string>("Local")!;
	}
}