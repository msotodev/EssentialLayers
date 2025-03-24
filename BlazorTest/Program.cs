using BlazorTest.Components;
using BlazorTest.Services.AspApi;
using BlazorTest.Services.Users;
using EssentialLayers.Request;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.UseRequest();

builder.Services.AddScoped<IAspApiService, AspApiService>();
builder.Services.AddScoped<IUsersService, UsersService>();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();