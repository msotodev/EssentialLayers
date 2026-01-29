using EssentialLayers.Dapper;
using EssentialLayers.Dapper.Options;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<ConnectionOption>().ValidateOnStart();

builder.Services.Configure<ConnectionOption>(
	options =>
	{
		options.ConnectionString = builder.Configuration.GetConnectionString("Local")!;
	}
);

builder.Services.UseDapper();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();